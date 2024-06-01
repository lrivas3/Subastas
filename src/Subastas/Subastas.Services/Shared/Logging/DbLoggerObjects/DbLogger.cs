using System.Diagnostics.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Subastas.Domain;
using Subastas.Interfaces.Repositories;

namespace Subastas.Services.Shared.Logging.DbLoggerObjects;

/// <summary>
/// Writes a log entry to the database.
/// </summary>
public class DbLogger : ILogger
{
    /// <summary>
    /// Instance of <see cref="DbLoggerProvider" />.
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    public DbLogger(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }

    /// <summary>
    /// Whether to log the entry.
    /// </summary>
    /// <param name="logLevel"></param>
    /// <returns></returns>
    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel != LogLevel.None;
    }


    /// <summary>
    /// Used to log the entry.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <param name="logLevel">An instance of <see cref="LogLevel"/>.</param>
    /// <param name="eventId">The event's ID. An instance of <see cref="EventId"/>.</param>
    /// <param name="state">The event's state.</param>
    /// <param name="exception">The event's exception. An instance of <see cref="Exception" /></param>
    /// <param name="formatter">A delegate that formats </param>
    public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
        Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            // Don't log the entry if it's not enabled.
            return;
        }

        using (var scope = _serviceProvider.CreateScope())
        {
            var logEntryRepository = scope.ServiceProvider.GetRequiredService<ILogEntryRepository>();
            // Store record.
            var logEntry = new LogEntry
            {
                Description = formatter(state, exception),
                Source = exception.Source ?? "General",
                DateLog = DateTime.Now,
                Type = logLevel.ToString(),
                Exception = exception?.ToString(),
                EventId = eventId.Id,
                EventName = eventId.Name,
                State = JsonConvert.SerializeObject(state),
                InnerExceptionMessage = exception?.InnerException?.Message
            };

            await logEntryRepository.AddLogAsync(logEntry);
        }
    }
}