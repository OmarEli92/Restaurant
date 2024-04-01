using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services,
                                                                   IConfiguration configuration)
        {
            services.AddDbContext<MyDBContext>(conf => conf.UseSqlServer(
                                                configuration.GetConnectionString("MyDBContext")));

            /*
            services.AddDbContext<MyDBContext>((serviceProvider, options) =>
            {
                options.UseInternalServiceProvider(serviceProvider);
            });
            */
            services.AddScoped<DishRepository>();
            services.AddScoped<OrderRepository>();
            services.AddScoped<UserRepository>();
            return services;
        }
    }
}
