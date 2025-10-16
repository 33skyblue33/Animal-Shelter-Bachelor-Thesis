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
    internal class MessageRepository(IDatabaseContext databaseContext) : IMessageRepository
    {
        public async Task AddAsync(Message message)
        {
             await databaseContext.Messages.AddAsync(message);
        }

        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            return await databaseContext.Messages.ToListAsync();
        }

        public async Task<Message?> GetByIdAsync(long id)
        {
            return await databaseContext.Messages.FindAsync(id);
        }

        public void Remove(Message message)
        {
            databaseContext.Messages.Remove(message);
        }
    }
}
