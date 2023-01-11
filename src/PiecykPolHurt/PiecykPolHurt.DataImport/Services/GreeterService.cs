using Grpc.Core;
using PiecykPolHurt.DataImport;

namespace PiecykPolHurt.DataImport.Services;

public class ProductUpdaterService : ProductUpdater.ProductUpdaterBase
{
    private readonly ILogger<ProductUpdaterService> _logger;
    
         public ProductUpdaterService(ILogger<ProductUpdaterService> logger)
         {
             _logger = logger;
         }

         public override Task<ProductUpdateResponse> UpdateProduct(ProductUpdateRequest request,
             ServerCallContext context)
         {
             return Task.FromResult(new ProductUpdateResponse
             {
                Message = "Product " + request.Type.ProductType_ + " stock updated"
             });
         }

}