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

        CreateProductSendPointCommand GetCreateProductSendPointCommand(String productCode,
            String sendpointCode);

        bool MakeUpdate(IList<ProductSendPointUpdateDto> updates);

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
            var products = await _productService.GetAllProductsAsync();
            var sendPoints = await _sendPointService.GetAllSendPointsAsync();
            if (products.All(product => product.Id != _mapper.Map<SimpleProductDto>(command.Product).Id))
            {
                return false;
            }
            if (sendPoints.All(sendPoint => sendPoint.Id != _mapper.Map<SendPoint>(command.SendPoint).Id))
            {
                return false;
            }
            // if (validationResult.IsValid)
            // {
                var productSendPoint = _mapper.Map<ProductSendPoint>(command);
                _unitOfWork.ProductSendPointRepository.Add(productSendPoint);
                await _unitOfWork.SaveChangesAsync();
                return true;
            // }
            return false;
        }

        public CreateProductSendPointCommand GetCreateProductSendPointCommand(String productCode, String sendpointCode)
        {
            var product = _productService.GetProductByCode(productCode);
            var sendPoint = _sendPointService.GetSendPointByCodeAsync(sendpointCode);
            CreateProductSendPointCommand createProductSendPointCommand = new CreateProductSendPointCommand();
            createProductSendPointCommand.Product = product.Result;
            createProductSendPointCommand.SendPoint = sendPoint.Result;
            return createProductSendPointCommand;
        }

        public async Task<IList<ProductSendPoint>> GetAllProductSendPointsAsync()
        {
            var productSendPoints = _unitOfWork.ProductSendPointRepository.GetAll().AsNoTracking();

            return await productSendPoints
                .ProjectTo<ProductSendPoint>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public bool MakeUpdate(IList<ProductSendPointUpdateDto> updates)
        {
            var productSendPoints = _unitOfWork.ProductSendPointRepository.GetAll().AsNoTracking();
            var productSendPointsList = productSendPoints.ToList();
            foreach (var update in updates)
            {
                var productSendPoint = productSendPoints.FirstOrDefault(prodSendPoint => 
                    prodSendPoint.Product.Code.Equals(update.ProductCode)
                        && prodSendPoint.SendPoint.Code.Equals(update.SendPointCode));
                if (productSendPoint != null)
                {
                    return false;
                }
                else
                {
                    CreateProductSendPointCommand command =
                        GetCreateProductSendPointCommand(update.ProductCode, update.SendPointCode);
                    if (CreateProductSendPointAsync(command).Result == false) return false;
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