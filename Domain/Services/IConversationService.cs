using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Services
{
    public interface IConversationService
    {
        Task<Conversation?> GetByIdAsync(long id);
        Task<IEnumerable<Conversation>> GetAllByEmployeeIdAsync(long employeeId);
        Task<IEnumerable<Conversation>> GetAllByUserIdAsync(long userId);
        Task<bool> AddAsync(Conversation conversation);
        Task<bool> DeleteAsync(long id);
    }
}
