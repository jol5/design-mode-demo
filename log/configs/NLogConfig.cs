using NLog.Config;
using NLog.Targets;

namespace log.configs
{
    public static class NLogConfig
    {
        private static LoggingConfiguration _config;
        public static LoggingConfiguration GetNLogConfig()
        {
            _config = new LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile")
            {
                FileName = "/home/jol/logs/evo-demo-${shortdate}.log",
                Layout = "${longdate} [${uppercase:${level}}] ${event-properties:item=EventId.Id}${newline}位置：${callsite:className=True:methodName=True:fileName=True:includeSourcePath=True:skipFrames=1}${newline}${message}${newline}${exception}${newline}",
                ArchiveAboveSize=10485760,
                ArchiveNumbering=ArchiveNumberingMode.DateAndSequence,
                ConcurrentWrites=true,
                MaxArchiveFiles=100000,
                KeepFileOpen=false,
            };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole")
            {
                Layout = "${longdate} [${uppercase:${level}}] ${event-properties:item=EventId.Id}${newline}位置：${callsite:className=True:methodName=True:fileName=True:includeSourcePath=True:skipFrames=1}${newline}${message}${newline}${exception}${newline}"
            };
            
            // Rules for mapping loggers to targets            
            _config.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, logconsole);
            _config.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Debug, logfile);

            return _config;
        }
    }
}