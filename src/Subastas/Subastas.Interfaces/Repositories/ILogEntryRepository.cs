using Subastas.Domain;
namespace Subastas.Interfaces.Repositories;

public interface ILogEntryRepository
{
        Task AddLogAsync(LogEntry logEntry);
        Task<IEnumerable<LogEntry>> GetAllLogsAsync(int pageNumber, int pageSize);
        Task<LogEntry> GetLogByIdAsync(int id);
        Task<int> GetTotalLogsCountAsync();
}
