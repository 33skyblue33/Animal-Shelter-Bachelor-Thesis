using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Services
{
    public interface IDotationService
    {
        Task<Dotation> GetByIdAsync(long id);
        Task<IEnumerable<Dotation>> GetAllAsync();
        Task<bool> CreateAsync(Dotation dotation);
        Task<bool> SaveAsync();
        Task<bool> DeleteByIdAsync(long id);
    }
}
