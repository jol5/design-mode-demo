using System;
using EvoLog;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            try
            {
                // 创建依赖注入器
                var servicesCollection = new ServiceCollection();

                var servicesProvider = LogBuilder
                    .AddLogging(servicesCollection)
                    .AddTransient<Test>().BuildServiceProvider();

                Helper.servicesProvider = servicesProvider;
                
                using (servicesProvider as IDisposable)
                {
                   var runner = servicesProvider.GetRequiredService<Test>();
                   runner.DoAction("Action1");
                   logger.Info("创建缓存开始...");
                   
                   var _cacheContext = EvoCacheContext.GetInstance();

                   var _cache =_cacheContext.GetCacheInstance();
            
                   logger.Info(_cacheContext.GetCurCacheType());

                   var key = "test";
                   _cache.Get(key);
                   _cache.Exit(key);

                   _cache =_cacheContext.ChangeCacheType(CacheTypeEnum.Redis);
                   logger.Info(_cacheContext.GetCurCacheType());
                   _cache.Get(key);
                   _cache.Exit(key);
                   
                    Console.WriteLine("Press ANY key to exit");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                // NLog: catch any exception and log it.
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }
        }
    }
}
