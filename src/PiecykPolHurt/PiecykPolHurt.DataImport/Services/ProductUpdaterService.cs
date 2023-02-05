using System.Collections;
using Grpc.Core;
using PiecykPolHurt.ApplicationLogic.Services;
using PiecykPolHurt.Model.Commands;
using PiecykPolHurt.Model.Dto.ProductSendPoint;
using PiecykPolHurt.Model.Entities;

namespace PiecykPolHurt.DataImport.Services;

public class ProductUpdaterService : ProductUpdater.ProductUpdaterBase
{
    private readonly IProductSendPointService _productSendPointService;
    
    public ProductUpdaterService(IProductSendPointService productSendPointService)
    {
        _productSendPointService = productSendPointService;
    }

    public override async Task<ProductUpdateResponse> UpdateProduct(ProductUpdateRequest request,
        ServerCallContext context)
    {
       
        var updateDtos = request.ProductUpdate.Select(productUpdate => new ProductSendPointUpdateDto(
                productUpdate.ProductType,
                productUpdate.SendPoint,
                productUpdate.Quantity,
                DateTime.Parse(productUpdate.Date),
                Convert.ToDecimal(productUpdate.Price))).ToList();

        bool updated = await _productSendPointService.MakeUpdate(updateDtos);

        return new ProductUpdateResponse
        {
            Message = request.ProductUpdate.Count + " products stock updated: " + updated
        };
    }

}