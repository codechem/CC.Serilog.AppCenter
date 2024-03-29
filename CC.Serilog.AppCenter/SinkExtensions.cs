using System;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;

namespace CC.Serilog.AppCenter
{
    public static class SinkExtensions
    {
        public static LoggerConfiguration AppCenterSink(
            this LoggerSinkConfiguration loggerConfiguration,
            string appCenterSecret,
            IFormatProvider formatProvider = null,
            LogEventLevel minimumLogLevel = LogEventLevel.Information,
            params Type[] types)
        {
            if (!string.IsNullOrEmpty(appCenterSecret))
            {
                Microsoft.AppCenter.AppCenter.Start(appCenterSecret, types);
            }
            
            return loggerConfiguration.Sink(new Sink(formatProvider, minimumLogLevel));
        }
    }
}