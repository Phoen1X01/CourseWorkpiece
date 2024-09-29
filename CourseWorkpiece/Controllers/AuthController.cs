using CourseWorkpiece.Data;
using CourseWorkpiece.Models;
using CourseWorkpiece.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CourseWorkpiece.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthService _authService;
        public AuthController(ApplicationDbContext context, IAuthService authService) 
        {
            _context = context;
            _authService = authService;
        }

        // GET: AuthController
        public async Task<ActionResult> Index()
        {
            User? user = await _authService.GetUser();
            if (user != null) return Redirect("/List");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string username, string password)
        {
            string passwordHes = ComputeSha256Hash(password);
            User? user = await _context.Users
                .Where(s => s.Login == username)
                .Where(s => s.PasswordHes == passwordHes)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                return View("Index");
            }


            Guid newGuid = Guid.NewGuid();
            string token = newGuid.ToString(); //создание токина

            var session = new Session();

            session.User = user;
            session.Token = token;

            _context.Sessions.Add(session); //запикаем это в бд
            _context.SaveChanges(); //коммит

            Response.Cookies.Append("token", token); //мы добавляем куки токен

            return Redirect("/List");
        }

        private static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }







    }

}