using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AppCenter.Crashes;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Configuration;

namespace CC.Serilog.AppCenter
{
    public class Sink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;
        private readonly LogEventLevel _logEventLevel;
        
        public Sink(
            IFormatProvider formatProvider,
            LogEventLevel logEventLevel)
        {
            _formatProvider = formatProvider;
            _logEventLevel = logEventLevel;
        }
        
        public void Emit(LogEvent logEvent)
        {
            var properties = new Dictionary<string, string>
            {
                { "level", logEvent.Level.ToString() },
                { "message", logEvent.RenderMessage() }
            };

            foreach (var (key, value) in logEvent.Properties)
            {
                properties.Add(key, value.ToString());
            }

            var exception = logEvent.Exception ?? new Exception(logEvent.Properties["0"].ToString());

            Crashes.TrackError(exception, properties);
        }
    }
}