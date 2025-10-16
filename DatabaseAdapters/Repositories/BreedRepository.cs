using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Ports.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAdapters.Repositories
{
    internal class BreedRepository(IDatabaseContext databaseContext) : IBreedRepository
    {
        public async Task AddAsync(Breed breed)
        {
            await databaseContext.Breeds.AddAsync(breed);
        }

        public async Task<IEnumerable<Breed>> GetAllAsync()
        {
            return await databaseContext.Breeds.ToListAsync();
        }

        public async Task<Breed?> GetByIdAsync(long id)
        {
            return await databaseContext.Breeds.FindAsync(id);
        }

        public void Remove(Breed breed)
        {
            databaseContext.Breeds?.Remove(breed);
        }
    }
}
