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
    internal class AdoptionRequestRepository(IDatabaseContext databasedContext) : IAdoptionRequestRepository
    {
        public async Task AddAsync(AdoptionRequest adoptionRequest)
        {
             await databasedContext.AdoptionRequests.AddAsync(adoptionRequest);
        }

        public async Task<IEnumerable<AdoptionRequest>> GetAllAsync()
        {
            return await databasedContext.AdoptionRequests.ToListAsync();
        }

        public async Task<AdoptionRequest?> GetByIdAsync(long id)
        {
            return await databasedContext.AdoptionRequests.FindAsync(id);
        }

        public void Remove(AdoptionRequest adoptionRequest)
        {
            databasedContext.AdoptionRequests.Remove(adoptionRequest);
        }
    }
}
