using CourseWorkpiece.Data;
using CourseWorkpiece.Models.Stats;
using CourseWorkpiece.Models;
using CourseWorkpiece.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseWorkpiece.Controllers
{
    public class StatController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<StatController> _logger;
        private readonly IAuthService _authService;
        private readonly IStatsService _statsService;

        public StatController(
            ApplicationDbContext context,
            ILogger<StatController> logger,
            IAuthService authService,
            IStatsService statsService
            )
        {
            _context = context;
            _logger = logger;
            _authService = authService;
            _statsService = statsService;
        }


        public async Task<IActionResult> Index()
        {
            User? user = await _authService.GetUser();
            if (user == null) return Redirect("/");

            return View();
        }


        public async Task<IActionResult> stats()
        {
            User? user = await _authService.GetUser();
            if (user == null) return Redirect("/");

            StatsModel stats = await _statsService.GetStat(user.sGroup);

            return Json(stats);
        }
    }
}
