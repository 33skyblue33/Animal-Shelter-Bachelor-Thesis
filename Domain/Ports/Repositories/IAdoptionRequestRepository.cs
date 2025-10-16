using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Ports.Repositories
{
    public interface IAdoptionRequestRepository
    {
        Task<AdoptionRequest?> GetByIdAsync(long id);
        Task<IEnumerable<AdoptionRequest>> GetAllAsync();
        Task AddAsync(AdoptionRequest adoptionRequest);
        void Remove(AdoptionRequest adoptionRequest);
    }
}
