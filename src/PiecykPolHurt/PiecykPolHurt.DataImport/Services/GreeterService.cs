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

    public override async Task<ProductUpdateResponse> UpdateProduct(ProductUpdateRequest request,
        ServerCallContext context)
    {
        var parsedDate = DateTime.Parse(request.ProductUpdate[0].Date);
        var updateDtos = request.ProductUpdate.Select(productUdpate => new ProductSendPointUpdateDto(
                productUdpate.ProductType,
                productUdpate.SendPoint,
                productUdpate.Quantity,
                DateTime.Parse(productUdpate.Date),
                Convert.ToDecimal(productUdpate.Price))).ToList();
        // var updateRequest = request.ProductUpdate.ToList();

        //zamiana na ten select wy¿ej
        //foreach (var productUdpate in request.ProductUpdate)
        //{
        //    updateDtos.Add(new ProductSendPointUpdateDto(
        //        productUdpate.ProductType,
        //        productUdpate.SendPoint,
        //        productUdpate.Quantity,
        //        DateTime.Parse(productUdpate.Date),
        //        Convert.ToDecimal(productUdpate.Price)));
        //}

        bool updated = await _productSendPointService.MakeUpdate(updateDtos);

        // _logger.LogInformation("date: " + parsedDate);
        // ArrayList productUpdateList =
        // CreateProductSendPointCommand createProductSendPointCommand =
        //     _productSendPointService.GetCreateProductSendPointCommand()

        // List<ProductSendPoint> productSendpointsList =
        //     await _productSendPointService.MakeUpdate(request.ProductUpdate); 

        //Tomek - jak return nie pyknie to spróbuj to bo nie wiem co to mia³o robiæ
        //return await Task.FromResult(new ProductUpdateResponse
        //{
        //    Message = request.ProductUpdate.Count + " products stock updated: " + updated
        //});

        return new ProductUpdateResponse
        {
            Message = request.ProductUpdate.Count + " products stock updated: " + updated
        };
    }

}