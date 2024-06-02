using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Subastas.Interfaces.Repositories;

namespace Subastas.Services.Shared.Logging.DbLoggerObjects
{
    [ProviderAlias("Database")]
    public class DbLoggerProvider : ILoggerProvider
    {
        public readonly IServiceProvider _ServiceProvider;

        public DbLoggerProvider(IServiceProvider serviceProvider)
        {
            _ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Creates a new instance of the db logger.
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName)
        {
            return new DbLogger(_ServiceProvider);
        }

        public void Dispose()
        {
        }
    }
}
