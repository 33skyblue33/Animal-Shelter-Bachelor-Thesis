using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Ports.Repositories;
using Domain.Util;

namespace Domain.Services
{
    public class MessageService(IMessageRepository messageRepository, IUnitOfWork unitOfWork) : IMessageService
    {
        public async Task<bool> CreateAsync(Message message)
        {
            await messageRepository.AddAsync(message);
            return await unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            Message? message = await messageRepository.GetByIdAsync(id);

            if (message is null)
            {
                return false;
            }

            messageRepository.Remove(message);
            return await unitOfWork.CompleteAsync() > 0;
        }

       
        public async Task<Message?> GetByIdAsync(long id)
        {
            return await messageRepository.GetByIdAsync(id);
        }

        public async Task<PagedResult<Message>> GetPagedByConversationIdAsync(long conversationId, int page, int pageSize)
        {
            return await messageRepository.GetPagedByConversationIdAsync(conversationId, page, pageSize);
        }

        public async Task<bool> SaveAsync()
        {
            return await unitOfWork.CompleteAsync() > 0;
        }
    }
}
