using Microsoft.EntityFrameworkCore;
using Subastas.Database;
using Subastas.Domain;
using Subastas.Interfaces.Repositories;

namespace Subastas.Repositories;

public class LogEntryRepository : ILogEntryRepository
{
    private readonly SubastasContext _context;

    public LogEntryRepository(SubastasContext context)
    {
        _context = context;
    }

    public async Task AddLogAsync(LogEntry logEntry)
    {
        await _context.LogEntries.AddAsync(logEntry);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<LogEntry>> GetAllLogsAsync(int pageNumber, int pageSize)
    {
        return await _context.LogEntries
            .OrderByDescending(log => log.DateLog)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<LogEntry> GetLogByIdAsync(int id)
    {
        var result = await _context.LogEntries.FindAsync(id);
        if (result == null)
        {
            throw new KeyNotFoundException($"No se encontro el id {id}");
        }

        return result;
    }
    public async Task<int> GetTotalLogsCountAsync()
    {
        return await _context.LogEntries.CountAsync();
    }
}