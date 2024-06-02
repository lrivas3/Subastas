using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;
using Subastas.Interfaces.Repositories;

namespace Subastas.Controllers
{
    public class LogEntriesController : Controller
    {
        private readonly ILogEntryRepository _repository;

        public LogEntriesController(ILogEntryRepository repository)
        {
            _repository = repository;
        }

        // GET: LogEntries
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            var logs = await _repository.GetAllLogsAsync(pageNumber, pageSize);
            var totalLogs = await _repository.GetTotalLogsCountAsync();

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalLogs = totalLogs;

            return View(logs);
        }

        public async Task<IActionResult> Details(int id)
        {
            var log = await _repository.GetLogByIdAsync(id);
            if (log == null)
            {
                return NotFound();
            }
            return View(log);
        }
    }
}