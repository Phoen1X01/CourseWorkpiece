using CourseWorkpiece.Data;
using CourseWorkpiece.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWorkpiece.Services
{
    public interface IAuthService
    {
        Task<User?> GetUser();
    }
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<User?> GetUser()
        {
            var httpContext = _httpContextAccessor.HttpContext; //получаем контекст запроса

            string? cookieValue = httpContext?.Request.Cookies["token"]; // мы с httpcontext получаем request и из coocie токен который мы сохранили у пользователя
            if (cookieValue == null)
            {
                return null;
            }

            Session? session = await _context.Sessions
                .Include(s => s.User)          // Включаем пользователя
                    .ThenInclude(u => u.sGroup) // Включаем 
                .Include(s => s.User)          // Включаем сессии пользователя
                    .ThenInclude(u => u.Sessions)
            .Where(s => s.Token == cookieValue).FirstOrDefaultAsync();

            if (session == null)
            {
                return null;
            }

            return session.User;
        }
    }
}
