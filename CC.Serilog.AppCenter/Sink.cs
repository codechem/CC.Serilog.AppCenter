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
        private readonly LogEventLevel _minimumLogLevel;
        
        public Sink(
            IFormatProvider formatProvider,
            LogEventLevel minimumLogLevel)
        {
            _formatProvider = formatProvider;
            _minimumLogLevel = minimumLogLevel;
        }
        
        public void Emit(LogEvent logEvent)
        {
            if (logEvent.Level < _minimumLogLevel) return;

            var properties = new Dictionary<string, string>
            {
                { "level", logEvent.Level.ToString() },
                { "message", logEvent.RenderMessage() }
            };

            foreach (var property in logEvent.Properties)
            {
                properties.Add(property.Key, property.Value.ToString());
            }

            var exception = logEvent.Exception ?? new Exception(logEvent.Properties["0"].ToString());

            Crashes.TrackError(exception, properties);
        }
    }
}