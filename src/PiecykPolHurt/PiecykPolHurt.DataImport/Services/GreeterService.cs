using Grpc.Core;

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
        // var parsedDate = DateTime.Parse(request.ProductUpdate[0].Date);
        // _logger.LogInformation("date: " + parsedDate);
        
        return Task.FromResult(new ProductUpdateResponse
        {
            Message = request.ProductUpdate.Count+ " products stock updated"
        });
    }

}