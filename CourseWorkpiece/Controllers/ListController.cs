using CourseWorkpiece.Data;
using CourseWorkpiece.Models;
using CourseWorkpiece.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseWorkpiece.Controllers
{
    public class ListController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthService _authService;
        public ListController(ApplicationDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }
        // GET: ListController1
        public async Task<ActionResult> Index() //асинхронность, а await это подождать асинхронную операцию
        {

            User? user = await _authService.GetUser();
            if (user == null) return Redirect("/");

            return View();
        }

        
    }
}
