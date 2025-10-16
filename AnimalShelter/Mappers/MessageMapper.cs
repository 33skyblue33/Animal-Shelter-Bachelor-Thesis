using AnimalShelter.Dto;
using Domain.Entities;

namespace AnimalShelter.Mappers
{
    public static class MessageMapper
    {
        public static MessageDto ToDto(this Message message)
        {
            return new MessageDto(message.Id, message.Content);
        }

        public static Message ToEntity(this MessageRequest messageRequest)
        {
            return new Message()
            {
                Content = messageRequest.Content
            };
        }

        public static void Map(this Message message, MessageRequest messageRequest)
        {
            message.Content = messageRequest.Content;
        }

    }
}
