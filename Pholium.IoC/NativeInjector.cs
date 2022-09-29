using Microsoft.Extensions.DependencyInjection;
using Pholium.Application.Interfaces;
using Pholium.Application.Services;

namespace Pholium.IoC
{
    public static class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}