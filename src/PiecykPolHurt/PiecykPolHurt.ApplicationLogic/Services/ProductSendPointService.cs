using System.Data;
using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PiecykPolHurt.DataLayer.Common;
using PiecykPolHurt.Model.Commands;
using PiecykPolHurt.Model.Dto;
using PiecykPolHurt.Model.Dto.ProductSendPoint;
using PiecykPolHurt.Model.Entities;
using PiecykPolHurt.Model.Enums;
using PiecykPolHurt.Model.Queries;



namespace PiecykPolHurt.ApplicationLogic.Services
{
    public interface IProductSendPointService
    {
        Task<bool> CreateProductSendPointAsync(CreateProductSendPointCommand command);
        Task<IList<ProductSendPoint>> GetAllProductSendPointsAsync(bool forToday = true);
        Task<bool> UpdateProductSendPointAsync(UpdateProductSendPointCommand command);

        Task<CreateProductSendPointCommand> GetCreateProductSendPointCommand(ProductSendPointUpdateDto updateDto);
        
        Task<UpdateProductSendPointCommand> GetUpdateProductSendPointCommand(ProductSendPointUpdateDto updateDto);

        Task<bool> MakeUpdate(IList<ProductSendPointUpdateDto> updates);

    }

    public class ProductSendPointService : IProductSendPointService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateProductSendPointCommand> _createValidator;
        private readonly IValidator<UpdateProductSendPointCommand> _updateValidator;
        private readonly IProductService _productService;
        private readonly ISendPointService _sendPointService;

        public ProductSendPointService(IMapper mapper, IUnitOfWork unitOfWork,
            IValidator<CreateProductSendPointCommand> createValidator,
            IValidator<UpdateProductSendPointCommand> updateValidator,
            IProductService productService, ISendPointService sendPointService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _productService = productService;
            _sendPointService = sendPointService;
        }


        public async Task<bool> CreateProductSendPointAsync(CreateProductSendPointCommand command)
        {
            var validationResult = await _createValidator.ValidateAsync(command);

            if (validationResult.IsValid)
            {
            var productSendPoint = _mapper.Map<ProductSendPoint>(command);
                _unitOfWork.ProductSendPointRepository.Add(productSendPoint);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }
        
        public async Task<CreateProductSendPointCommand> GetCreateProductSendPointCommand(ProductSendPointUpdateDto updateDto)
        {
            var product = await _unitOfWork.ProductRepository.GetByCode(updateDto.ProductCode);
            var sendPoint = await _unitOfWork.SendPointRepository.GetByCode(updateDto.SendPointCode);
            if(product == null)
            {
                throw new Exception("Product is null");
            }
            if(sendPoint == null)
            {
                throw new Exception("SendPoint is null");
            }

            return new CreateProductSendPointCommand
            {
                ProductId = product.Id,
                SendPointId = sendPoint.Id,
                AvailableQuantity = updateDto.AvailableQuantity,
                ForDate = updateDto.ForDate,
            };
        }

        public async Task<IList<ProductSendPoint>> GetAllProductSendPointsAsync(bool forToday = true)
        {
            var productSendPoints = await _unitOfWork.ProductSendPointRepository.GetAll().AsNoTracking()
                .ProjectTo<ProductSendPoint>(_mapper.ConfigurationProvider)
                .ToListAsync();
            if (forToday)
            {
                productSendPoints =
                    productSendPoints.FindAll(prodSendPoint => prodSendPoint.ForDate.Equals(DateTime.Today));

            }

            return productSendPoints;
        }

        public async Task<bool> MakeUpdate(IList<ProductSendPointUpdateDto> updates)
        {
            foreach (var update in updates)
            {
                //Jeśli istnieje jakikolwiek ProductSendPoint z takim produktem i send pointem
                if (
                    await _unitOfWork.ProductSendPointRepository
                    .GetAll()
                    .Include(x=> x.Product)
                    .Include(x => x.SendPoint)
                    .AnyAsync(prodSendPoint =>
                        prodSendPoint.Product.Code.Equals(update.ProductCode)
                        && prodSendPoint.SendPoint.Code.Equals(update.SendPointCode)
                        )
                    )
                {

                    try
                    {
                        UpdateProductSendPointCommand command = await GetUpdateProductSendPointCommand(update);
                        if (command == null) continue;
                        if (await UpdateProductSendPointAsync(command) == false) return false;
                    }
                    catch (RowNotInTableException e)
                    {
                        return false;
                    }
                }
                else
                {
                    CreateProductSendPointCommand command = await GetCreateProductSendPointCommand(update);
                    if (await CreateProductSendPointAsync(command) == false) return false;
                }
            }
            
            return true;
        }
        
        public async Task<UpdateProductSendPointCommand> GetUpdateProductSendPointCommand(ProductSendPointUpdateDto updateDto)
        {
            ProductSendPoint? productSendPoint =  await _unitOfWork.ProductSendPointRepository
                .GetAll()
                .Include(x => x.Product)
                .Include(x => x.SendPoint)
                .FirstOrDefaultAsync(prodSendPoint =>
                    prodSendPoint.Product.Code.Equals(updateDto.ProductCode)
                    && prodSendPoint.SendPoint.Code.Equals(updateDto.SendPointCode)
                );
            if (productSendPoint == null)
            {
                throw new RowNotInTableException("No record to update found: Product code" + updateDto.ProductCode + "SendPoint code: " + updateDto.SendPointCode);
            }

            if (productSendPoint.AvailableQuantity.Equals(updateDto.AvailableQuantity)
                && productSendPoint.ForDate.Date.Equals(updateDto.ForDate.Date)) return null;
            
            return new UpdateProductSendPointCommand
            {
                Id = productSendPoint.Id,
                ProductId = productSendPoint.ProductId,
                SendPointId = productSendPoint.SendPointId,
                AvailableQuantity = updateDto.AvailableQuantity,
                ForDate = updateDto.ForDate,
            };
        }

        public async Task<bool> UpdateProductSendPointAsync(UpdateProductSendPointCommand command)
        {
            var validationResult = await _updateValidator.ValidateAsync(command);
            if (validationResult.IsValid)
            {
                var productSendPoint = await _unitOfWork.ProductSendPointRepository.GetById(command.Id).FirstOrDefaultAsync();
                productSendPoint = _mapper.Map(command, productSendPoint);
                _unitOfWork.ProductSendPointRepository.Update(productSendPoint);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}