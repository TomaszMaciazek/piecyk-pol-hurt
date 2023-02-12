using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PiecykPolHurt.API.Extensions;
using PiecykPolHurt.ApplicationLogic;
using PiecykPolHurt.DataLayer;
using PiecykPolHurt.Mappings;
using PiecykPolHurt.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultPolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAuth(builder.Configuration);

builder.Services.AddDataLayer(builder.Configuration);
builder.Services.AddMappings();
builder.Services.AddValidation();
builder.Services.AddLogic();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("DefaultPolicy");

app.UseHttpsRedirection();
app.UseAuth();

app.MapControllers();

await app.Services.GetService<ApplicationDbContext>().Database.MigrateAsync();

app.Run();