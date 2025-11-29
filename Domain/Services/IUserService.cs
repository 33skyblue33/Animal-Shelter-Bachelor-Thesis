using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Util;

namespace Domain.Services
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(long id);
        Task<User?> CreateAsync(User user);
        Task<bool> DeleteByIdAsync(long id);
        Task<bool> DeleteAllInactiveAsync();  
        Task<bool> UpdateByIdAsync(long id, UserUpdateCommand data);
    }
}
