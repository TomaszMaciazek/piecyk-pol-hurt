using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PiecykPolHurt.ApplicationLogic.Extensions;
using PiecykPolHurt.ApplicationLogic.Result;
using PiecykPolHurt.DataLayer.Common;
using PiecykPolHurt.Model.Commands;
using PiecykPolHurt.Model.Dto.Order;
using PiecykPolHurt.Model.Entities;
using PiecykPolHurt.Model.Enums;
using PiecykPolHurt.Model.Queries;

namespace PiecykPolHurt.ApplicationLogic.Services
{
    public interface IOrderService
    {
        Task<bool> ApproveOrderAsync(ApproveOrderCommand command, string userEmail);
        Task<bool> CancelOrderAsync(int id, string userEmail);
        Task<bool> CreateOrderAsync(CreateOrderCommand model, int userId, string userEmail);
        Task<bool> FinishOrderAsync(int id, string userEmail);
        Task<OrderDto> GetOrderById(int id);
        Task<PaginatedList<OrderListItemDto>> GetOrders(OrderQuery query);
        Task<bool> RejectOrderAsync(int id, string userEmail);
        Task<bool> SetReceptionDateAsync(int id, DateTime date, string userEmail);
    }

    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateOrderCommand> _createValidator;

        public OrderService(IMapper mapper, IUnitOfWork unitOfWork, IValidator<CreateOrderCommand> createValidator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _createValidator = createValidator;
        }

        public async Task<PaginatedList<OrderListItemDto>> GetOrders(OrderQuery query)
        {
            var orders = _unitOfWork.OrderRepository.GetAll()
                .Include(x => x.Buyer)
                .Include(x => x.SendPoint)
                .Include(x => x.Lines).ThenInclude(x => x.Product)
                .AsNoTracking();

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
                var order = new Order
                {
                    BuyerId = userId,
                    RequestedReceptionDate = model.RequestedReceptionDate,
                    ReceptionDate = null,
                    Buyer = null,
                    Created = DateTime.Now,
                    CreatedBy = userEmail,
                    SendPointId = model.SendPointId,
                    Status = Model.Enums.OrderStatus.Sent,
                    Lines = new List<OrderLine>()
                };
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
                }
                _unitOfWork.OrderRepository.Add(order);
                await _unitOfWork.SaveChangesAsync();
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
                var order = await _unitOfWork.OrderRepository.GetById(command.Id).FirstOrDefaultAsync();
                if (order != null)
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
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> RejectOrderAsync(int id, string userEmail)
        {
            if (id != 0)
            {
                var order = await _unitOfWork.OrderRepository.GetById(id).FirstOrDefaultAsync();
                if (order != null)
                {
                    order.Status = OrderStatus.Rejected;
                    order.Modified = DateTime.Now;
                    order.ModifiedBy = userEmail;
                    _unitOfWork.OrderRepository.Update(order);
                    await _unitOfWork.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> FinishOrderAsync(int id, string userEmail)
        {
            if (id != 0)
            {
                var order = await _unitOfWork.OrderRepository.GetById(id).FirstOrDefaultAsync();
                if (order != null)
                {
                    order.Status = OrderStatus.Finished;
                    order.Modified = DateTime.Now;
                    order.ModifiedBy = userEmail;
                    _unitOfWork.OrderRepository.Update(order);
                    await _unitOfWork.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> CancelOrderAsync(int id, string userEmail)
        {
            if (id != 0)
            {
                var order = await _unitOfWork.OrderRepository.GetById(id).FirstOrDefaultAsync();
                if (order != null)
                {
                    order.Status = OrderStatus.Canceled;
                    order.Modified = DateTime.Now;
                    order.ModifiedBy = userEmail;
                    _unitOfWork.OrderRepository.Update(order);
                    await _unitOfWork.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }
    }
}
