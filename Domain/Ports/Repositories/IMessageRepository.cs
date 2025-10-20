using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Util;

namespace Domain.Ports.Repositories
{
    public interface IMessageRepository
    {
        Task<Message?> GetByIdAsync(long id);
        Task<PagedResult<Message>> GetPagedByConversationIdAsync(long conversationId, int page, int pageSize);
        Task AddAsync(Message message);
        void Remove(Message message);
    }
}
