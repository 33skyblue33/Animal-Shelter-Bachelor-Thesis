using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Ports.Repositories
{
    public interface IMessageRepository
    {
        Task<Message?> GetByIdAsync(long id);
        Task<IEnumerable<Message>> GetAllAsync();
        Task AddAsync(Message message);
        void Remove(Message message);
    }
}
