using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Util;

namespace Domain.Services
{
    public interface IMessageService
    {
        Task<Message?> GetByIdAsync(long id);
        Task<PagedResult<Message>> GetPagedByConversationIdAsync(long conversationId, int page, int pageSize);
        Task<bool> CreateAsync(Message message);
        Task<bool> SaveAsync();
        Task<bool> DeleteAsync(long id);

    }
}
