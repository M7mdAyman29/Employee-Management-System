using EMS.Domain.Entities;
using EMS.Infrastructure.Data;
using EMS.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Infrastructure.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Employee)
                   .ThenInclude(e => e.Role)
        .Include(u => u.Employee)
            .ThenInclude(e => e.Department)
                .FirstOrDefaultAsync(u => u.Email == email);

        }

    }
}