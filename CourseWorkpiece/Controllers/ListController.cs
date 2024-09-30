using CourseWorkpiece.Data;
using CourseWorkpiece.Models;
using CourseWorkpiece.Models.List;
using CourseWorkpiece.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CourseWorkpiece.Controllers
{
    public class ListController : Controller
    {
		private readonly ApplicationDbContext _context;
		private readonly ILogger<ListController> _logger;
		private readonly IAuthService _authService;
		private readonly ILessonService _lessonService;

		public ListController(
			ApplicationDbContext context, 
			ILogger<ListController> logger, 
			IAuthService authService, 
			ILessonService lessonService)
		{
			_context = context;
			_logger = logger;
			_authService = authService;
			_lessonService = lessonService;
		}

		// GET: ListController1
		public async Task<ActionResult> Index() //асинхронность, а await это подождать асинхронную операцию
		{
			User? user = await _authService.GetUser();
			if (user == null) return Redirect("/");

			List<Traffic> traffics = await _lessonService.GetTrafficFilteredByGroupLesson(user.sGroup);

			return View(traffics);
        }

		[HttpPut]
		public async Task<IActionResult> EditorAttendance()
		{
			User? user = await _authService.GetUser();
			if (user == null) return Redirect("/");

			using (StreamReader reader = new StreamReader(Request.Body))
			{
				string body = await reader.ReadToEndAsync();

				var model = JsonSerializer.Deserialize<EditorTrafficModel>(body);

				if (model == null)
				{
					return BadRequest("Invalid JSON data.");
				}

				Traffic? att = await _context.Traffics.Where(s => s.Id == model.id).FirstOrDefaultAsync();
				if (att == null)
				{
					return BadRequest("Invalid id.");
				}

				att.TypeTraffic = model.Traffic;
				await _context.SaveChangesAsync();

				return Ok("Data updated successfully.");
			}
		}

		[HttpPut]
		public async Task<IActionResult> DeleteStudent()
		{
			User? user = await _authService.GetUser();
			if (user == null) return Redirect("/");

			using (StreamReader reader = new StreamReader(Request.Body))
			{
				string body = await reader.ReadToEndAsync();

				var model = JsonSerializer.Deserialize<DeleteStudentModel>(body);

				if (model == null)
				{
					return BadRequest("Invalid JSON data.");
				}

				Student? att = await _context.Students.Where(s => s.Id == model.id).FirstOrDefaultAsync();
				if (att == null)
				{
					return BadRequest("Invalid id.");
				}

				_context.Students.Remove(att);
				await _context.SaveChangesAsync();

				return Ok("Data updated successfully.");
			}
		}

		public async Task<IActionResult> New(string FirstName, string LastName, string? MiddleName)
		{
			User? user = await _authService.GetUser();
			if (user == null) return Redirect("/");

			Student student = new Student();
			student.FirstName = FirstName;
			student.LastName = LastName;
			student.MiddleName = MiddleName;

			student.sGroup = user.sGroup;
			student.sGroupId = user.sGroupId;

			_context.Students.Add(student);

			List<Lecture> lessons = await _context.Lectures.ToListAsync();

            foreach (var lesson in lessons)
            {
				Traffic traffic = new Traffic();
				traffic.TypeTraffic = Enums.TypeTraffic.None;

				traffic.StudentId = student.Id;
				traffic.Student = student;

				traffic.LectureId = lesson.Id;
				traffic.Lecture = lesson;

				_context.Traffics.Add(traffic);
			}

			await _context.SaveChangesAsync();

			return Redirect("/List");
		}
	}
}
