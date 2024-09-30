using CourseWorkpiece.Controllers;
using CourseWorkpiece.Data;
using CourseWorkpiece.Enums;
using CourseWorkpiece.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CourseWorkpiece.Services
{
    public interface ILessonService
    {
        public DateOnly GetTime();
        public int GetPair();
        public Task<Lecture> GetLesson(DateOnly? time = null, int? pair = null);

        public Task<List<Traffic>> GetTrafficFilteredByGroupLesson(sGroup group, DateOnly? time = null, int? pair = null);
        public Task<List<Traffic>> GetTrafficFilteredByGroupLesson(sGroup group, Lecture lesson);
    }

    public class LessonService : ILessonService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LessonService> _logger;

        public LessonService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context, ILogger<LessonService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _logger = logger;
        }


        private async Task<Lecture> SendLesson(DateOnly time, int pair)
        {
			Lecture? lesson = await _context.Lectures
				.Include(s => s.Traffics)
					.ThenInclude(u => u.Student)
					.ThenInclude(u => u.sGroup)
				.Where(s => s.Number == pair)
                .Where(s => s.Date == time)
                .FirstOrDefaultAsync();
            return lesson;
        }

        public async Task<Lecture?> GetLesson(DateOnly? time = null, int? pair = null)
        {
            if (time == null)
                time = GetTime();
            if (pair == null)
                pair = GetPair();

			Lecture? lecture = await SendLesson((DateOnly)time, (int)pair);
            if (lecture != null)
                return lecture;

            if (((DateOnly)time).DayOfWeek == DayOfWeek.Saturday || ((DateOnly)time).DayOfWeek == DayOfWeek.Sunday)
            {
                //Console.WriteLine("Это выходной день (суббота или воскресенье).");
                return null;
            }

			//crete
			lecture = new Lecture();

			lecture.Number = (int)pair;
			lecture.Date = (DateOnly)time;

            _context.Lectures.Add(lecture);
            _context.SaveChanges();

            List<Student> students = await _context.Students.ToListAsync();

            foreach (var student in students)
            {
				Traffic newTraffic = new Traffic();
                newTraffic.TypeTraffic = TypeTraffic.None;

                newTraffic.Student = student;
                newTraffic.StudentId = student.Id;

                newTraffic.Lecture = lecture;
                newTraffic.LectureId = lecture.Id;
                _context.Traffics.Add(newTraffic);
            }

            _context.SaveChanges();

			lecture = await SendLesson((DateOnly)time, (int)pair);
            if (lecture != null)
                return lecture;
            throw new InvalidOperationException("lesson null");

        }


        public int GetPair()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            string? pairQuery = httpContext?.Request.Query["pair"];

            if (!string.IsNullOrEmpty(pairQuery))
                return Convert.ToInt32(pairQuery);

            DateTime currentDate = DateTime.Now;

            if (currentDate.Hour < 10)
                return 1;
            else if (currentDate.Hour < 12)
                return 2;
            else if (currentDate < DateTime.Today.AddHours(13).AddMinutes(50))
                return 3;
            else if (currentDate < DateTime.Today.AddHours(15).AddMinutes(30))
                return 4;
            else if (currentDate < DateTime.Today.AddHours(17).AddMinutes(10))
                return 5;
            else if (currentDate.Hour < 18)
                return 6;
            else if (currentDate < DateTime.Today.AddHours(19).AddMinutes(35))
                return 7;
            return 8;
        }

        public DateOnly GetTime()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            DateTime currentDate = DateTime.Now;


            string? yearQuery = httpContext?.Request.Query["year"];
            int year = currentDate.Year;
            if (!string.IsNullOrEmpty(yearQuery))
                year = Convert.ToInt32((string)yearQuery);


            string? monthQuery = httpContext?.Request.Query["month"];
            int month = currentDate.Month;
            if (!string.IsNullOrEmpty(monthQuery))
                month = Convert.ToInt32((string)monthQuery);


            string? dayQuery = httpContext?.Request.Query["day"];
            int day = currentDate.Day;
            if (!string.IsNullOrEmpty(dayQuery))
                day = Convert.ToInt32((string)dayQuery);


            DateOnly date = new DateOnly(year, month, day);
            return date;
        }

        public async Task<List<Traffic>> GetTrafficFilteredByGroupLesson(sGroup group, DateOnly? time = null, int? pair = null)
        {
			Lecture? lecture = await GetLesson(time, pair);
            if (lecture == null)
                return new List<Traffic>();
            return await GetTrafficFilteredByGroupLesson(group, lecture);
        }

        public async Task<List<Traffic>> GetTrafficFilteredByGroupLesson(sGroup group, Lecture lecture)
        {
            if (lecture == null)
                return new List<Traffic>();

            List<Traffic> users = new List<Traffic>();
            foreach (var item in lecture.Traffics)
            {
                if (item.Student.sGroupId == group.Id)
                {
                    users.Add(item);
                }
            }
            return users;
        }
    }
}
