using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Ports.Repositories
{
    public interface IBreedRepository
    {
        Task<Breed?> GetByIdAsync(long id);
        Task<IEnumerable<Breed>> GetAllAsync();
        Task AddAsync(Breed breed);
        void Remove(Breed breed);

    }
}
