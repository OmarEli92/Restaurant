using Application.Abstractions.Services;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services,
                                                                IConfiguration configuration) {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDishService, DishService>();
            services.AddScoped<IOrderService, OrderService>();
            return services;
        }
    }
}
