using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Ports.Repositories;
using Domain.Util;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAdapters.Repositories
{
    internal class UserRepository(IDatabaseContext databaseContext) : IUserRepository
    {
        public async Task AddAsync(User user)
        {
            await databaseContext.Users.AddAsync(user);
        }

        public async Task<IEnumerable<User>> GetAllInactiveAsync()
        {
            return await databaseContext.Users.Where(u => u.Role == UserRole.Unverified).ToListAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await databaseContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdAsync(long id)
        {
            return await databaseContext.Users.FindAsync(id);
        }

        public void Remove(User user)
        {
            databaseContext.Users.Remove(user);
        }
    }
}
