using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Ports.Repositories;
using Domain.Util;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAdapters.Repositories
{
    internal class MessageRepository(IDatabaseContext databaseContext) : IMessageRepository
    {
        public async Task AddAsync(Message message)
        {
             await databaseContext.Messages.AddAsync(message);
        }

        public async Task<Message?> GetByIdAsync(long id)
        {
            return await databaseContext.Messages.FindAsync(id);
        }

        public async Task<PagedResult<Message>> GetPagedByConversationIdAsync(long conversationId, int page, int pageSize)
        {
            int totalCount = await databaseContext.Messages
                .Where(m => m.ConversationId == conversationId)
                .CountAsync();

            IEnumerable<Message> messages = await databaseContext.Messages
                .Where(m => m.ConversationId == conversationId)
                .OrderByDescending(m => m.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Message>(messages, totalCount, page, pageSize);
        }

        public void Remove(Message message)
        {
            databaseContext.Messages.Remove(message);
        }
    }
}
