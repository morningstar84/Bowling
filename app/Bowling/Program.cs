using System;
using System.IO;
using Bowling.Domain.Interfaces;
using Bowling.IoC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using NLog.Web;

namespace Bowling
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Info("[PROGRAM]: started");
                var builder = CreateHostBuilder(args).Build();
                using (var scope = builder.Services.CreateScope())
                {
                    var provider = scope.ServiceProvider;
                    var gameService = provider.GetService<IGameService>()!;
                    
                    var uiManager = provider.GetService<IUiManager>()!;
                    
                    uiManager.Welcome();
                    while (!gameService.IsGameFinished())
                    {
                        var value = uiManager.RequestRoll();
                        var result = gameService.Roll(value);
                        if (!result)
                        {
                            uiManager.AlertWrongRoll();
                            continue;
                        }
                        var results = gameService.GetResults();
                        uiManager.PrintFramesResults(results);
                    }
                    uiManager.PrintCurrentScore(gameService.GetCurrentScore(), gameService.IsGameFinished());
                }

                logger.Info("[PROGRAM]: finished");
            }
            catch (Exception e)
            {
                logger.Error(e, "Stopped program because of exception");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var path = Directory.GetCurrentDirectory();
                    DependencyContainer.CreateAndRegisterServices(path, services, args);
                });
        }
    }
}