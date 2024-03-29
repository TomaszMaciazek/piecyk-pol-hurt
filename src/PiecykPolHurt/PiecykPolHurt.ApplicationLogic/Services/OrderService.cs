﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PiecykPolHurt.ApplicationLogic.Extensions;
using PiecykPolHurt.ApplicationLogic.Result;
using PiecykPolHurt.DataLayer.Common;
using PiecykPolHurt.EmailService;
using PiecykPolHurt.Model.Commands;
using PiecykPolHurt.Model.Dto.Order;
using PiecykPolHurt.Model.Entities;
using PiecykPolHurt.Model.Enums;
using PiecykPolHurt.Model.Queries;
using System;

namespace PiecykPolHurt.ApplicationLogic.Services
{
    public interface IOrderService
    {
        Task<bool> ApproveOrderAsync(ApproveOrderCommand command, string userEmail);
        Task<bool> CancelOrderAsync(int id, string userEmail);
        Task<bool> CreateOrderAsync(CreateOrderCommand model, int userId, string userEmail);
        Task<bool> FinishOrderAsync(int id, string userEmail);
        Task<OrderDto> GetOrderById(int id);
        Task<PaginatedList<OrderListItemDto>> GetOrders(OrderQuery query, int? buyerId = null);
        Task<bool> RejectOrderAsync(int id, string userEmail);
        Task<bool> SetReceptionDateAsync(int id, DateTime date, string userEmail);
    }

    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateOrderCommand> _createValidator;
        private readonly IEmailService _emailService;

        public OrderService(IMapper mapper, IUnitOfWork unitOfWork, IValidator<CreateOrderCommand> createValidator, IEmailService emailService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _createValidator = createValidator;
            _emailService = emailService;
        }

        public async Task<PaginatedList<OrderListItemDto>> GetOrders(OrderQuery query, int? buyerId = null)
        {
            var orders = _unitOfWork.OrderRepository.GetAll()
                .Include(x => x.Buyer)
                .Include(x => x.SendPoint)
                .Include(x => x.Lines).ThenInclude(x => x.Product)
                .AsNoTracking();

            if (buyerId.HasValue)
            {
                orders = orders.Where(x => x.BuyerId == buyerId.Value);
            }

            if (query.SendPoints != null && query.SendPoints.Any())
            {
                orders = orders.Where(x => query.SendPoints.Contains(x.SendPointId));
            }

            if (query.Statuses != null && query.Statuses.Any())
            {
                orders = orders.Where(x => query.Statuses.Contains(x.Status));
            }

            if (query.SortOption.HasValue)
            {
                orders = query.SortOption switch
                {
                    OrderSortOption.DateDesc => orders.OrderByDescending(x => x.Created),
                    OrderSortOption.DateAsc => orders.OrderBy(x => x.Created),
                    OrderSortOption.PriceAsc => orders.OrderBy(x => x.Lines.Select(x => x.PriceForOneItem * x.ItemsQuantity).Sum()),
                    OrderSortOption.PriceDesc => orders.OrderByDescending(x => x.Lines.Select(x => x.PriceForOneItem * x.ItemsQuantity).Sum()),
                    OrderSortOption.SendPointCodeAsc => orders.OrderBy(x => x.SendPoint.Code),
                    OrderSortOption.SendPointCodeDesc => orders.OrderByDescending(x => x.SendPoint.Code),
                    _ => orders.OrderByDescending(x => x.Created)
                };
            }
            else
            {
                orders = orders.OrderByDescending(x => x.Created);
            }

            return await orders.ProjectTo<OrderListItemDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(query.PageNumber, query.PageSize);
        }

        public async Task<OrderDto> GetOrderById(int id)
            => await _unitOfWork.OrderRepository.GetById(id)
            .AsNoTracking()
            .Include(x => x.Lines)
            .ThenInclude(x => x.Product)
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        public async Task<bool> CreateOrderAsync(CreateOrderCommand model, int userId, string userEmail)
        {
            var validationResult = await _createValidator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var notificationTemplate = await _unitOfWork.NotificationTypeRepository.GetByType(NotificationType.OrderCreated).FirstOrDefaultAsync();
                if(notificationTemplate == null)
                {
                    return false;
                }
                var mail = new SendEmailRequest { To = userEmail, Subject = notificationTemplate.Subject };

                using var transaction = await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var order = new Order
                    {
                        BuyerId = userId,
                        RequestedReceptionDate = model.RequestedReceptionDate,
                        ReceptionDate = null,
                        Buyer = null,
                        Created = DateTime.Now,
                        CreatedBy = userEmail,
                        SendPointId = model.SendPointId,
                        Status = OrderStatus.Sent,
                        Lines = new List<OrderLine>()
                    };

                    var today = DateTime.Now.Date;

                    foreach (var line in model.Lines)
                    {
                        order.Lines.Add(new OrderLine
                        {
                            ItemsQuantity = line.ItemsQuantity,
                            PriceForOneItem = line.PriceForOneItem,
                            ProductId = line.ProductId,
                            Product = null,
                            Order = null,
                            OrderId = 0
                        });


                        var productSendPoint = await _unitOfWork.ProductSendPointRepository.GetAll()
                            .Where(x => x.ForDate.Date == today)
                            .FirstOrDefaultAsync(x => x.ProductId == line.ProductId && x.SendPointId == order.SendPointId);

                        if(productSendPoint.AvailableQuantity - line.ItemsQuantity < 0)
                        {
                            throw new Exception("Not enough quantity for product - transaction rollbacked");
                        }
                        productSendPoint.AvailableQuantity -= line.ItemsQuantity;
                        _unitOfWork.ProductSendPointRepository.Update(productSendPoint);
                    }
                    _unitOfWork.OrderRepository.Add(order);
                    await _unitOfWork.SaveChangesAsync();
                    await transaction.CommitAsync();
                    mail.Content = string.Format(notificationTemplate.Body, order.Id);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
                await _emailService.SendEmail(mail);
                return true;
            }
            return false;
        }

        public async Task<bool> SetReceptionDateAsync(int id, DateTime date, string userEmail)
        {
            if (id != 0)
            {
                var order = await _unitOfWork.OrderRepository.GetById(id).FirstOrDefaultAsync();
                if (order != null)
                {
                    order.ReceptionDate = date;
                    order.Modified = DateTime.Now;
                    order.ModifiedBy = userEmail;
                    _unitOfWork.OrderRepository.Update(order);
                    await _unitOfWork.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> ApproveOrderAsync(ApproveOrderCommand command, string userEmail)
        {
            if (command.Id != 0)
            {
                using var transaction = await _unitOfWork.BeginTransactionAsync();
                var order = await _unitOfWork.OrderRepository.GetById(command.Id).FirstOrDefaultAsync();
                var notificationTemplate = await _unitOfWork.NotificationTypeRepository.GetByType(NotificationType.OrderApproved).FirstOrDefaultAsync();
                if (order != null && notificationTemplate != null)
                {
                    var buyer = await _unitOfWork.UserRepository.GetById(order.BuyerId).AsNoTracking().FirstOrDefaultAsync();
                    try
                    {

                        order.Status = OrderStatus.Approved;
                        order.Modified = DateTime.Now;
                        order.ModifiedBy = userEmail;
                        if (command.ReceptionDate.HasValue)
                        {
                            order.ReceptionDate = command.ReceptionDate.Value;
                        }
                        _unitOfWork.OrderRepository.Update(order);
                        await _unitOfWork.SaveChangesAsync();
                        await _emailService.SendEmail(new SendEmailRequest
                        {
                            To = buyer.Email,
                            Subject = string.Format(notificationTemplate.Subject, order.Id),
                            Content = string.Format(notificationTemplate.Body, order.Id)
                        });
                        await transaction.CommitAsync();
                        return true;
                    }
                    catch(Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
            return false;
        }

        public async Task<bool> RejectOrderAsync(int id, string userEmail)
        {
            if (id != 0)
            {
                using var transaction = await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var order = await _unitOfWork.OrderRepository.GetById(id).Include(x => x.Lines).FirstOrDefaultAsync();
                    var notificationTemplate = await _unitOfWork.NotificationTypeRepository.GetByType(NotificationType.OrderRejected).FirstOrDefaultAsync();
                    if (order != null && notificationTemplate != null)
                    {
                        var buyer = await _unitOfWork.UserRepository.GetById(order.BuyerId).AsNoTracking().FirstOrDefaultAsync();
                        order.Status = OrderStatus.Rejected;
                        order.Modified = DateTime.Now;
                        order.ModifiedBy = userEmail;
                        _unitOfWork.OrderRepository.Update(order);
                        var today = DateTime.Now.Date;
                        if (order.Created.Date == today) {
                            foreach (var line in order.Lines)
                            {
                                var productSendPoint = await _unitOfWork.ProductSendPointRepository.GetAll()
                                .Where(x => x.ForDate.Date == today)
                                .FirstOrDefaultAsync(x => x.ProductId == line.ProductId && x.SendPointId == order.SendPointId);
                                productSendPoint.AvailableQuantity += line.ItemsQuantity;
                                _unitOfWork.ProductSendPointRepository.Update(productSendPoint);
                            }
                        }
                        await _unitOfWork.SaveChangesAsync();
                        await _emailService.SendEmail(new SendEmailRequest
                        {
                            To = buyer.Email,
                            Subject = string.Format(notificationTemplate.Subject, order.Id),
                            Content = string.Format(notificationTemplate.Body, order.Id)
                        });
                        await transaction.CommitAsync();
                        return true;
                    }
                }
                catch(Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            return false;
        }

        public async Task<bool> FinishOrderAsync(int id, string userEmail)
        {
            if (id != 0)
            {
                using var transaction = await _unitOfWork.BeginTransactionAsync();
                var notificationTemplate = await _unitOfWork.NotificationTypeRepository.GetByType(NotificationType.OrderFinished).FirstOrDefaultAsync();
                var order = await _unitOfWork.OrderRepository.GetById(id).FirstOrDefaultAsync();
                if (order != null && notificationTemplate != null)
                {
                    try
                    {
                        var buyer = await _unitOfWork.UserRepository.GetById(order.BuyerId).AsNoTracking().FirstOrDefaultAsync();
                        order.Status = OrderStatus.Finished;
                        order.Modified = DateTime.Now;
                        order.ModifiedBy = userEmail;
                        _unitOfWork.OrderRepository.Update(order);
                        await _unitOfWork.SaveChangesAsync();
                        await _emailService.SendEmail(new SendEmailRequest
                        {
                            To = buyer.Email,
                            Subject = string.Format(notificationTemplate.Subject, order.Id),
                            Content = string.Format(notificationTemplate.Body, order.Id)
                        });
                        await transaction.CommitAsync();
                        return true;
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
            return false;
        }

        public async Task<bool> CancelOrderAsync(int id, string userEmail)
        {
            if (id != 0)
            {
                using var transaction = await _unitOfWork.BeginTransactionAsync();
                var notificationTemplate = await _unitOfWork.NotificationTypeRepository.GetByType(NotificationType.OrderCanceled).FirstOrDefaultAsync();
                var order = await _unitOfWork.OrderRepository.GetById(id).FirstOrDefaultAsync();
                if (order != null && notificationTemplate!= null)
                {
                    try
                    {
                        order.Status = OrderStatus.Canceled;
                        order.Modified = DateTime.Now;
                        order.ModifiedBy = userEmail;
                        _unitOfWork.OrderRepository.Update(order);
                        await _unitOfWork.SaveChangesAsync();
                        await _emailService.SendEmail(new SendEmailRequest
                        {
                            To = userEmail,
                            Subject = string.Format(notificationTemplate.Subject, order.Id),
                            Content = string.Format(notificationTemplate.Body, order.Id)
                        });
                        await transaction.CommitAsync();
                        return true;
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
            return false;
        }
    }
}
