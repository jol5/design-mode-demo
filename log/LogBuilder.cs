
using log.configs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace EvoLog
{
    public class LogBuilder
    {
        public static IServiceCollection AddLogging(IServiceCollection serviceCollection)
        {
            var config = NLogConfig.GetNLogConfig();
            // Apply config           
            NLog.LogManager.Configuration = config;
            
            return serviceCollection
                .AddLogging(loggingBuilder =>
               {
                   // configure Logging with NLog
                   loggingBuilder.ClearProviders();
                   loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                   loggingBuilder.AddNLog(config);
               });
        }
    }
}
