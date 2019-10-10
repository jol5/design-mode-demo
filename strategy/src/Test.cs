using Microsoft.Extensions.Logging;

namespace strategy
{
    public class Test
    {
        private readonly ILogger<Test> _logger;

        public Test(ILogger<Test> logger)
        {
            _logger = logger;
        }

        public void DoAction(string name)
        {
            _logger.LogDebug(20, "Doing hard work! {Action}", name);
            _logger.LogError(20, "Doing hard work! {Action}", name);
            _logger.LogTrace(20, "Doing hard work! {Action}", name);
            _logger.LogInformation(20, "Doing hard work! {Action}", name);
        }
    }
}