using Abstractions.Services;
using Application.Abstractions;
using Application.Abstractions.Services;
using Application.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using UApplication.Options;

namespace Application.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services,
                                                                IConfiguration configuration) {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDishService, DishService>();
            services.AddScoped<IOrderService, OrderService>();   
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            var hashingOptions = new HashingOptions();
            services.AddTransient<HashingOptions>();
            configuration.GetSection("HashingSalting").Bind(hashingOptions);
            services.AddScoped<IHashingService, HashingService>();
            services.Configure<HashingOptions>(configuration.GetSection("HashingSalting"));
            services.AddValidatorsFromAssembly(
            AppDomain.CurrentDomain.GetAssemblies().
            SingleOrDefault(assembly => assembly.GetName().Name == "Unicam.Paradigmi.118301.Application"));
            
            return services;
        }


    }
}
