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
    internal class PetRepository(IDatabaseContext databaseContext) : IPetRepository
    {
        

        public async Task AddAsync(Pet pet)
        {
            await databaseContext.Pets.AddAsync(pet);
        }

        public async Task<IEnumerable<Pet>> GetAllAsync()
        {
            return await databaseContext.Pets.ToListAsync();
        }

        public async Task<Pet?> GetByIdAsync(long id)
        {
            return await databaseContext.Pets.FindAsync(id);
        }

        public void Remove(Pet pet)
        {
            databaseContext.Pets?.Remove(pet);
        }
    }
}
