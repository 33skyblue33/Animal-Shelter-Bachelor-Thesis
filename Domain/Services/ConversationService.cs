using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Ports.Repositories;

namespace Domain.Services
{
    public class ConversationService(IUnitOfWork unitOfWork, IConversationRepository conversationRepository) : IConversationService
    {
        public async Task<bool> AddAsync(Conversation conversation)
        {
            await conversationRepository.AddAsync(conversation);
            return await unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            Conversation? conversation = await conversationRepository.GetByIdAsync(id);
            if(conversation != null)
            {
                return false;
            }

            conversationRepository.Remove(conversation);
            return await unitOfWork.CompleteAsync() > 0;
        }

        public async Task<IEnumerable<Conversation>> GetAllByEmployeeIdAsync(long employeeId)
        {
            return await conversationRepository.GetAllByEmployeeIdAsync(employeeId);
        }

        public async Task<IEnumerable<Conversation>> GetAllByUserIdAsync(long userId)
        {
            return await conversationRepository.GetAllByUserIdAsync(userId);
        }

        public async Task<Conversation?> GetByIdAsync(long id)
        {
            return await conversationRepository.GetByIdAsync(id);
        }
    }
}
