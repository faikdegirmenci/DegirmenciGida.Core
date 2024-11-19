using Serilog;
using Serilog.Events;

namespace Infrastructure.LoggingServices
{
    public static class LoggingService
    {
        public static void ConfigureLogging(string connectionString)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Microsoft log seviyesini ayarlama
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.File("logs/app_log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.MSSqlServer(
                    connectionString: connectionString,
                    tableName: "Logs",
                    autoCreateSqlTable: true,
                    restrictedToMinimumLevel: LogEventLevel.Error // MSSQL için hata ve üzeri logları kaydet
                )
                .CreateLogger();
        }

        public static void LogInformation(string message)
        {
            Log.Information(message);
        }

        public static void LogError(Exception ex, string message)
        {
            Log.Error(ex, message);
        }

        public static void CloseAndFlush()
        {
            Log.CloseAndFlush();
        }
    }
}
