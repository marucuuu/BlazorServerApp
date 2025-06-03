// Services/UserService.cs
using BlazorServerApp.Data;
using BlazorServerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerApp.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Users>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
