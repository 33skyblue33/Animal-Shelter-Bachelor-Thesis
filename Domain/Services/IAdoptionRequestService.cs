using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Services
{
    public interface IAdoptionRequestService
    {
        Task<AdoptionRequest?> GetByIdAsync(long id);
        Task<IEnumerable<AdoptionRequest>> GetAllAsync();
        Task<bool> CreateAsync(AdoptionRequest adoptionRequest);
        Task<bool> UpdateAsync(long id, AdoptionRequest adoptionRequest);
        Task<bool> DeleteAsync(long id);
    }
}
