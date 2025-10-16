using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Ports.Repositories
{
    public interface IDotationRepository
    {
        Task<Dotation?> GetByIdAsync(long id);
        Task<IEnumerable<Dotation>> GetAllAsync();
        Task AddAsync(Dotation dotation);
        void Remove(Dotation dotation);
    }
}
