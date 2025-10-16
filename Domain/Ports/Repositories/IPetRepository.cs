using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Ports.Repositories
{
    public interface IPetRepository
    {
        Task<Pet?> GetByIdAsync(long id);
        Task<IEnumerable<Pet>> GetAllAsync();
        Task AddAsync(Pet pet);
        void Remove(Pet pet);
    }
}
