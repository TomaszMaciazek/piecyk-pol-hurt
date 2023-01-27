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
            return services;
        }
    }
}