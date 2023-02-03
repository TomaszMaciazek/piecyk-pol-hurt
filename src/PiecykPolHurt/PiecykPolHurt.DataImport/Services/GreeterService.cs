using System.Collections;
using Grpc.Core;
using PiecykPolHurt.ApplicationLogic.Services;
using PiecykPolHurt.Model.Commands;
using PiecykPolHurt.Model.Dto.ProductSendPoint;
using PiecykPolHurt.Model.Entities;

namespace PiecykPolHurt.DataImport.Services;

public class ProductUpdaterService : ProductUpdater.ProductUpdaterBase
{
    private readonly ILogger<ProductUpdaterService> _logger;
    // private readonly IProductService _productService;
    // private readonly ISendPointService _sendPointService;
    private readonly IProductSendPointService _productSendPointService;
    
    public ProductUpdaterService(ILogger<ProductUpdaterService> logger,
        // IProductService productService
        // ISendPointService sendPointService,
        IProductSendPointService productSendPointService
        )
    {
        _logger = logger;
        // _productService = productService;
        // _sendPointService = sendPointService;
        _productSendPointService = productSendPointService;
    }

    public override Task<ProductUpdateResponse> UpdateProduct(ProductUpdateRequest request,
        ServerCallContext context)
    {
        var parsedDate = DateTime.Parse(request.ProductUpdate[0].Date);
        var updateDtos = new List<ProductSendPointUpdateDto>();
        // var updateRequest = request.ProductUpdate.ToList();
        foreach (var productUdpate in request.ProductUpdate)
        {
            updateDtos.Add(new ProductSendPointUpdateDto(
                productUdpate.ProductType,
                productUdpate.SendPoint,
                productUdpate.Quantity,
                DateTime.Parse(productUdpate.Date),
                Convert.ToDecimal(productUdpate.Price)));
        }

        bool updated = _productSendPointService.MakeUpdate(updateDtos);

        // _logger.LogInformation("date: " + parsedDate);
        // ArrayList productUpdateList =
            // CreateProductSendPointCommand createProductSendPointCommand =
            //     _productSendPointService.GetCreateProductSendPointCommand()
            
            // List<ProductSendPoint> productSendpointsList =
            //     await _productSendPointService.MakeUpdate(request.ProductUpdate); 
        
        return Task.FromResult(new ProductUpdateResponse
        {
            Message = request.ProductUpdate.Count + " products stock updated: " + updated
        });
    }

}