using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Ports.Repositories
{
    public interface IConversationRepository
    {
        Task<Conversation?> GetByIdAsync(long id);
        Task<IEnumerable<Conversation>> GetAllByUserIdAsync(long userId);
        Task<IEnumerable<Conversation>> GetAllByEmployeeIdAsync(long employeeId);
        Task AddAsync(Conversation conversation);
        void Remove(Conversation conversation);
    }
}
