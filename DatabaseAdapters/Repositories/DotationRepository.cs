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
    internal class DotationRepository(IDatabaseContext databaseContext) : IDotationRepository
    {
        public async Task<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Dotation>> AddAsync(Dotation dotation)
        {
            return await databaseContext.Dotations.AddAsync(dotation);
        }

        public async Task<IEnumerable<Dotation>> GetAllAsync()
        {
            return await databaseContext.Dotations.ToListAsync();
        }

        public async Task<Dotation?> GetByIdAsync(long id)
        {
            return await databaseContext.Dotations.FindAsync(id);
        }

        public void Remove(Dotation dotation)
        {
            databaseContext.Dotations?.Remove(dotation);
        }

        Task IDotationRepository.AddAsync(Dotation dotation)
        {
            return AddAsync(dotation);
        }
    }
}
