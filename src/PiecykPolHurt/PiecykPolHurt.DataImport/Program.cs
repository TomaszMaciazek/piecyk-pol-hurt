using PiecykPolHurt.ApplicationLogic;
using PiecykPolHurt.DataImport.Services;
using PiecykPolHurt.DataLayer;
using PiecykPolHurt.EmailService;
using PiecykPolHurt.Mappings;
using PiecykPolHurt.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddDataLayer(builder.Configuration);
builder.Services.AddMappings();
builder.Services.AddValidation();
builder.Services.AddLogic();
builder.Services.AddEmailService(builder.Configuration);

var app = builder.Build();

app.MapGrpcService<ProductUpdaterService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();