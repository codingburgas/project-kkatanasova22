using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Controllers
{
    using LibraryManager.Services.Contracts;
    using LibraryManagers.Core.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "Admin,Librarian")]
    public class StatisticsController : Controller
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController
        (IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        public async Task<IActionResult> Dashboard()
        {
            ViewBag.TopBooks = await _statisticsService.GetTop5BorrowedBooksAsync();
            ViewBag.Overdue = await _statisticsService.GetOverdueReadersAsync();
            var stats = await _statisticsService.GetGeneralStatisticsAsync();

            return View(stats);
        }
    }
}
