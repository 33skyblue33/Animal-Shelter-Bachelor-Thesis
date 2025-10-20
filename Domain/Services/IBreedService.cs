using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Services
{
    public interface IBreedService
    {
        Task<Breed?> GetByIdAsync(long id);
        Task<IEnumerable<Breed>> GetAllAsync();
        Task<bool> CreateAsync(Breed breed);
        Task<bool> DeleteByIdAsync(long id);
        Task<bool> SaveAsync();
    }
}
