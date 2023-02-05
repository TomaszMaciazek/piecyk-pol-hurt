using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PiecykPolHurt.Model.Commands;
using PiecykPolHurt.Validation.Validators;

namespace PiecykPolHurt.Validation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateReportDefinitionCommand>, CreateReportDefinitionCommandValidator>();
            services.AddScoped<IValidator<UpdateReportDefinitionCommand>, UpdateReportDefinitionCommandValidator>();

            services.AddScoped<IValidator<CreateSendPointCommand>, CreateSendPointCommandValidator>();
            services.AddScoped<IValidator<UpdateSendPointCommand>, UpdateSendPointCommandValidator>();

            services.AddScoped<IValidator<UpdateProductCommand>, UpdateProductCommandValidator>();
            services.AddScoped<IValidator<CreateProductCommand>, CreateProductCommandValidator>();

            services.AddScoped<IValidator<CreateOrderLineCommand>, CreateOrderLineCommandValidator>();
            services.AddScoped<IValidator<CreateOrderCommand>, CreateOrderCommandValidator>();
            
            services.AddScoped<IValidator<CreateProductSendPointCommand>, CreateProductSendPointCommandValidator>();
            services.AddScoped<IValidator<UpdateProductSendPointCommand>, UpdateProductSendPointCommandValidator>();
            return services;
        }
    }
}