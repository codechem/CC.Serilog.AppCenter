1. Install Nuget Package
2. Add the Sink when initializing your Serilog instance:
    ```csharp
    Log.Logger = new LoggerConfiguration()
                .WriteTo.AppCenterSink( {APP_CENTER_IDENTIFIER} )
                .CreateLogger();
    ```

Available arguments:
```csharp
public static LoggerConfiguration AppCenterSink(
            this LoggerSinkConfiguration loggerConfiguration,
            string appCenterSecret,
            IFormatProvider formatProvider = null,
            LogEventLevel minimumLogLevel = LogEventLevel.Information,
            params Type[] types)
```
