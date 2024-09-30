using CourseWorkpiece.Controllers;
using CourseWorkpiece.Data;
using CourseWorkpiece.Models;
using CourseWorkpiece.Models.Stats;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CourseWorkpiece.Services
{
    public interface IStatsService
    {
        public Task<StatsModel> GetStat(sGroup group);
    }

    public class StatsService : IStatsService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<StatsService> _logger;

        private readonly ILessonService _getLessonService;

        public StatsService(
                IHttpContextAccessor httpContextAccessor,
                ApplicationDbContext context,
                ILogger<StatsService> logger,
                ILessonService getLessonService
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _logger = logger;
            _getLessonService = getLessonService;
        }

        public async Task<StatsModel> GetStat(sGroup group)
        {
            StatsModel stats = new StatsModel();


            // data
            DateOnly today = _getLessonService.GetTime();
            int pair = _getLessonService.GetPair();

            //iterator
            var startDate = new DateOnly(DateTime.Now.Year, 9, 1);
            if (startDate > today)
                startDate = startDate.AddYears(-1);


            for (var data = startDate; data < today; data = data.AddDays(1))
            {
                for (int i = 1; i <= pair; i++)
                {
                    await DataAnalitic(group,stats, data, i);
                }
            }

            for (int i = 1; i <= pair; i++)
            {
                await DataAnalitic(group, stats, today, i);
            }
            return stats;
        }

        private async Task DataAnalitic(sGroup group, StatsModel stats, DateOnly data, int pair)
        {
            List<Traffic> traffics = await 
                _getLessonService.GetTrafficFilteredByGroupLesson(group, data, pair);

            foreach (var traffic in traffics)
            {
                stats.Traffics[traffic.TypeTraffic] += 1;

                if (!stats.Students.ContainsKey(traffic.StudentId))
                    stats.Students[traffic.StudentId] = new StudentDataModel(traffic.Student);

                stats.Students[traffic.StudentId].Traffics[traffic.TypeTraffic] += 1;
            }

        }
    }
}
