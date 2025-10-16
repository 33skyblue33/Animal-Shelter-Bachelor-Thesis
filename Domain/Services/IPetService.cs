using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Services
{
    public interface IPetService
    {
        Task<Pet?> GetByIdAsync(long id);
        Task<IEnumerable<Pet>> GetAllAsync();
        Task<bool> CreateAsync(Pet pet);
        Task<bool> DeleteByIdAsync(long id);
        Task<bool> SaveAsync();
    }
}
