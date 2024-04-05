using Application.Abstractions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UApplication.Options;

namespace Application.Middleware
{
    public class HashingMiddleware
{
        private RequestDelegate _next;
        public HashingMiddleware(RequestDelegate next
            )
        {
            _next = next;

        }

        public async Task Invoke(HttpContext context,IUserService userService
                                , IConfiguration configuration, IOptions<HashingOptions> hashingOptions)
        {
            context.RequestServices.GetRequiredService<IUserService>();
            await _next.Invoke(context);
        }
    }
}
