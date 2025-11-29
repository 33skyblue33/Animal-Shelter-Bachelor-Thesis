using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Ports.Repositories;
using Microsoft.EntityFrameworkCore;


namespace DatabaseAdapters.Repositories
{
    internal class ConversationRepository(IDatabaseContext databaseContext) : IConversationRepository
    {
        public async Task AddAsync(Conversation conversation)
        {
             await databaseContext.Conversations.AddAsync(conversation);
        }

        public async Task<IEnumerable<Conversation>> GetAllByEmployeeIdAsync(long employeeId)
        {
            return await databaseContext.Conversations.Where(c => c.EmployeeId == employeeId).ToListAsync();
        }

        public async Task<IEnumerable<Conversation>> GetAllByUserIdAsync(long userId)
        {
            return await databaseContext.Conversations.Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<Conversation?> GetByIdAsync(long id)
        {
            return await databaseContext.Conversations.FindAsync();
        }

        public void Remove(Conversation conversation)
        {
           databaseContext.Conversations?.Remove(conversation);
        }
    }
}
