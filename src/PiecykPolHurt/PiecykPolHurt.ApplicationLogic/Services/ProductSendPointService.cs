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

        // Task<bool> DeleteProductAsync(int id);
        Task<IList<ProductSendPoint>> GetAllProductSendPointsAsync();

        // Task<ProductDto> GetProductByIdAsync(int id);
        // Task<PaginatedList<ProductListItemDto>> GetProductsAsync(ProductQuery query);
        Task<bool> UpdateProductSendPointAsync(UpdateProductSendpointCommand command);

        Task<CreateProductSendPointCommand> GetCreateProductSendPointCommand(string productCode, string sendpointCode);

        Task<bool> MakeUpdate(IList<ProductSendPointUpdateDto> updates);

    }

    public class ProductSendPointService : IProductSendPointService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        // private readonly IValidator<CreateProductSendPointCommand> _createValidator;
        // private readonly IValidator<UpdateProductSendpointCommand> _updateValidator;
        private readonly IProductService _productService;
        private readonly ISendPointService _sendPointService;

        public ProductSendPointService(IMapper mapper, IUnitOfWork unitOfWork,
            // IValidator<CreateProductSendPointCommand> createValidator,
            // IValidator<UpdateProductSendpointCommand> updateValidator,
            IProductService productService, ISendPointService sendPointService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            // _createValidator = createValidator;
            // _updateValidator = updateValidator;
            _productService = productService;
            _sendPointService = sendPointService;
        }


        public async Task<bool> CreateProductSendPointAsync(CreateProductSendPointCommand command)
        {
            // var validationResult = await _createValidator.ValidateAsync(command);

            // if (validationResult.IsValid)
            // {
            var productSendPoint = _mapper.Map<ProductSendPoint>(command);
                _unitOfWork.ProductSendPointRepository.Add(productSendPoint);
                await _unitOfWork.SaveChangesAsync();
                return true;
            // }
            return false;
        }

        public async Task<CreateProductSendPointCommand> GetCreateProductSendPointCommand(string productCode, string sendpointCode)
        {
            var product = await _unitOfWork.ProductRepository.GetByCode(productCode);
            var sendPoint = await _unitOfWork.SendPointRepository.GetByCode(sendpointCode);
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
            };
        }

        public async Task<IList<ProductSendPoint>> GetAllProductSendPointsAsync()
        {
            var productSendPoints = _unitOfWork.ProductSendPointRepository.GetAll().AsNoTracking();

            return await productSendPoints
                .ProjectTo<ProductSendPoint>(_mapper.ConfigurationProvider)
                .ToListAsync();
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
                    return false;
                }
                else
                {
                    CreateProductSendPointCommand command = await GetCreateProductSendPointCommand(update.ProductCode, update.SendPointCode);
                    return await CreateProductSendPointAsync(command);
                }
            }

            return true;
        }

        public async Task<bool> UpdateProductSendPointAsync(UpdateProductSendpointCommand command)
        {
            // var validationResult = await _updateValidator.ValidateAsync(command);
            // if (validationResult.IsValid)
            // {
                var productSendpoint = await _unitOfWork.ProductSendPointRepository.GetById(command.Id).FirstOrDefaultAsync();
                productSendpoint = _mapper.Map(command, productSendpoint);
                _unitOfWork.ProductSendPointRepository.Update(productSendpoint);
                await _unitOfWork.SaveChangesAsync();
                return true;
            // }
            return false;
        }
    }
}