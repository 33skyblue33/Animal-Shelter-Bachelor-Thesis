using AnimalShelter.Dto;
using Domain.Entities;

namespace AnimalShelter.Mappers
{
    public static class ConversationMapper
    {
        public static ConversationDto ToDto(this Conversation conversation)
        {
            return new ConversationDto(conversation.Id, conversation.UserId, conversation.EmployeeId);
        }

        public static Conversation ToEntity(this ConversationRequest conversationRequest)
        {
            return new Conversation()
            {
                EmployeeId = conversationRequest.EmployeeId,
                UserId = conversationRequest.UserId,
            };
        }

        public static void Map(this Conversation conversation, ConversationRequest conversationRequest)
        {
            conversation.EmployeeId = conversationRequest.EmployeeId;
            conversation.UserId = conversationRequest.UserId;
            
        }
    }
}

