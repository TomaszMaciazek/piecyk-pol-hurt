using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PiecykPolHurt.DataLayer.Common;
using PiecykPolHurt.Model.Dto;

namespace PiecykPolHurt.ApplicationLogic.Services
{
    public interface IProductService
    {
        Task<IList<SimpleProductDto>> GetAllProducts();
    }

    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<SimpleProductDto>> GetAllProducts() => await _unitOfWork.ProductRepository
            .GetAll()
            .ProjectTo<SimpleProductDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
