using Auto.BizLogic.Models.Dto;
using Auto.BizLogic.Services;
using Auto.BizLogic.Validators;
using Auto.Data;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auto.BizLogic
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoBizLogicServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAutoService, AutoService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOfferService, OfferService>();

            services.AddScoped<IValidator<CreateOfferDto>, CreateOfferDtoValidator>();

            services.AddAutoDataServices(configuration);
            return services;
        }
    }
}