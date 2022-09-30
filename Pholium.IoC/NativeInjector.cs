using Microsoft.Extensions.DependencyInjection;
using Pholium.Application.Interfaces;
using Pholium.Application.Services;
using Pholium.Data.Repositories;
using Pholium.Domain.Interfaces;

namespace Pholium.IoC
{
    public static class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Services
            services.AddScoped<IUserService, UserService>();
            #endregion

            #region Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion
        }
    }
}