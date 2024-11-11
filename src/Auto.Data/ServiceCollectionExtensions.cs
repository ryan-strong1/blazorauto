using Auto.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auto.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoDataServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AutoDbContext>(options =>
           options.UseSqlServer(configuration.GetConnectionString("AutoCS"),options => 
           { 
               options.UseNetTopologySuite();
               options.EnableRetryOnFailure(
                    maxRetryCount: 5, 
                    maxRetryDelay: TimeSpan.FromSeconds(30), 
                    errorNumbersToAdd: null); 
           }));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAutoRepository, AutoRepository>();
            services.AddScoped<IOfferRepository, OfferRepository>();

            return services;
        }
    }
}
