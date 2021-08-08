using Bowling.Domain.Interfaces;
using Bowling.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bowling.IoC
{
    public static class DependencyContainer
    {
        private static void RegisterServices(IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton(_ => config);
            services.AddSingleton<IGameService, GameService>();
            services.AddSingleton<IUiManager, UiManager>();
        }

        /// <summary>
        ///     It creates services if not provided
        /// </summary>
        /// <param name="configBasePath">appsettings.json </param>
        /// <returns>Collections of services</returns>
        public static IServiceCollection CreateAndRegisterServices(string configBasePath, IServiceCollection services,
            string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(configBasePath)
                .AddCommandLine(args)
                .AddJsonFile("appsettings.json", false, true);
            var config = builder.Build();
            RegisterServices(services, config);
            return services;
        }
    }
}