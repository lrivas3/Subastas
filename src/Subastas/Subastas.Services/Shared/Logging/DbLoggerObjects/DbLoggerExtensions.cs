using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Subastas.Interfaces.Repositories;
using Subastas.Repositories;

namespace Subastas.Services.Shared.Logging.DbLoggerObjects
{
    public static class DbLoggerExtensions
    {
        public static ILoggingBuilder AddDbLogger(this ILoggingBuilder builder, 
            Action<DbLoggerOptions> configure)
        {          
            builder.Services.AddSingleton<ILoggerProvider, DbLoggerProvider>();
            builder.Services.AddScoped<ILogEntryRepository, LogEntryRepository>();
            builder.Services.Configure(configure);
            return builder;
        }
    }
}
