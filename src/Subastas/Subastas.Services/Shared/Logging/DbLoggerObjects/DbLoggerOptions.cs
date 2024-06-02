namespace Subastas.Services.Shared.Logging.DbLoggerObjects
{
    public class DbLoggerOptions
    {
        public string ConnectionString { get; set; }

        public string[] LogFields { get; init; }

        public string LogTable { get; init; }

        public DbLoggerOptions()
        {
        }
    }
}
