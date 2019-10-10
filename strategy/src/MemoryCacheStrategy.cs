using NLog;

namespace strategy
{
    public class MemoryCacheStrategy: ICache
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();
        public MemoryCacheStrategy()
        {
        }
        
        public string Get(string key)
        {
            _logger.Info(this.GetType().Name);
            return this.GetType().FullName;
        }

        public bool Set(string key)
        {
            _logger.Info(this.GetType().Name);
            return true;
        }

        public bool Exit(string key)
        {
            _logger.Info(this.GetType().Name);
            return true;
        }
    }
}