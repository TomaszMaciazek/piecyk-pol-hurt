using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PiecykPolHurt.ApplicationLogic.Extensions;
using PiecykPolHurt.ApplicationLogic.Result;
using PiecykPolHurt.DataLayer.Common;
using PiecykPolHurt.Model.Commands;
using PiecykPolHurt.Model.Dto;
using PiecykPolHurt.Model.Entities;
using PiecykPolHurt.Model.Enums;
using PiecykPolHurt.Model.Queries;

namespace PiecykPolHurt.ApplicationLogic.Services
{
    public interface IProductService
    {
        Task<bool> CreateProductAsync(CreateProductCommand command, string user);
        Task<bool> DeleteProductAsync(int id);
        Task<IList<SimpleProductDto>> GetAllProductsAsync(bool onlyActive = true);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<PaginatedList<ProductListItemDto>> GetProductsAsync(ProductQuery query);
        Task<bool> UpdateProductAsync(UpdateProductCommand command, string user);
        Task<Product> GetProductByCode(String code);
    }

    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateProductCommand> _createValidator;
        private readonly IValidator<UpdateProductCommand> _updateValidator;

        public ProductService(IMapper mapper, IUnitOfWork unitOfWork, IValidator<CreateProductCommand> createValidator, IValidator<UpdateProductCommand> updateValidator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<PaginatedList<ProductListItemDto>> GetProductsAsync(ProductQuery query)
        {
            var products = _unitOfWork.ProductRepository.GetAll().AsNoTracking();

            if (!string.IsNullOrEmpty(query.Name))
            {
                products = products.Where(x => x.Name.ToLower().Contains(query.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(query.Code))
            {
                products = products.Where(x => x.Code.ToLower().Contains(query.Code.ToLower()));
            }

            if (query.IsActive.HasValue)
            {
                products = products.Where(x => x.IsActive == query.IsActive.Value);
            }

            products = query.SortOption switch
            {
                ProductSortOption.NameAsc => products.OrderBy(x => x.Name),
                ProductSortOption.NameDesc => products.OrderByDescending(x => x.Name),
                ProductSortOption.CodeAsc => products.OrderBy(x => x.Code),
                ProductSortOption.CodeDesc => products.OrderByDescending(x => x.Code),
                _ => products.OrderBy(x => x.Code)
            };

            return await products.ProjectTo<ProductListItemDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(query.PageNumber, query.PageSize);
        }

        public async Task<IList<SimpleProductDto>> GetAllProductsAsync(bool onlyActive = true)
        {
            var products = _unitOfWork.ProductRepository.GetAll().AsNoTracking();

            if (onlyActive)
            {
                products = products.Where(x => x.IsActive);
            }

            return await products
            .ProjectTo<SimpleProductDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
            => await _unitOfWork.ProductRepository.GetById(id)
            .AsNoTracking()
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        public async Task<Product> GetProductByCode(String code)
            => await _unitOfWork.ProductRepository.GetByCode(code);
        public async Task<bool> CreateProductAsync(CreateProductCommand command, string user)
        {
            var validationResult = await _createValidator.ValidateAsync(command);
            if (validationResult.IsValid)
            {
                var product = _mapper.Map<Product>(command);
                product.CreatedBy = user;
                _unitOfWork.ProductRepository.Add(product);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateProductAsync(UpdateProductCommand command, string user)
        {
            var validationResult = await _updateValidator.ValidateAsync(command);
            if (validationResult.IsValid)
            {
                var product = await _unitOfWork.ProductRepository.GetById(command.Id).FirstOrDefaultAsync();
                product = _mapper.Map(command, product);
                product.ModifiedBy = user;
                _unitOfWork.ProductRepository.Update(product);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            if (!await _unitOfWork.ProductRepository.WasProductUsedInOrder(id))
            {
                _unitOfWork.ProductRepository.Delete(id);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
