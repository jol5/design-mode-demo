using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace strategy
{
    public class EvoCacheContext
    {
        private static ICache _cache;
        private static readonly object locked = new object();
        static EvoCacheContext instance = null;

        private readonly ILogger<RedisCacheStrategy> _logger = Helper.servicesProvider.GetRequiredService<ILogger<RedisCacheStrategy>>();
        private EvoCacheContext(CacheTypeEnum cacheTypeEnum = CacheTypeEnum.Memory)
        {
            createCacheInstance(cacheTypeEnum);
        }

        public static EvoCacheContext GetInstance()
        {
                lock (locked)
                {
                    if (instance == null)
                    {
                        instance = new EvoCacheContext(CacheTypeEnum.Memory);
                    }
                }

                return instance;
        }

        public ICache ChangeCacheType(CacheTypeEnum cacheTypeEnum)
        {
            Console.WriteLine($"Try to switch cache stratety to {cacheTypeEnum.ToString()}");
            var suc = createCacheInstance(cacheTypeEnum, true);
            if(suc) return _cache;
            return null;
        }

        public ICache GetCacheInstance(CacheTypeEnum cacheTypeEnum = CacheTypeEnum.Memory)
        {
            var suc = createCacheInstance(cacheTypeEnum);
            if (suc)
                return _cache;

            return null;
        }
        private static bool createCacheInstance(CacheTypeEnum cacheTypeEnum, bool isChangeCacheType=false)
        {

            if (_cache != null && !isChangeCacheType) return true;

            try
            {
                lock (locked)
                {
                    if (cacheTypeEnum.Equals(obj: CacheTypeEnum.Redis))
                        _cache = new RedisCacheStrategy();
                    if (cacheTypeEnum.Equals(CacheTypeEnum.Memory))
                        _cache = new MemoryCacheStrategy();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        public string GetCurCacheType()
        {
            return _cache.GetType().Name;
        }
    }


    public enum CacheTypeEnum
    {
        Redis = 1,
        Memory = 2
    }
}