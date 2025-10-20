using AnimalShelter.Dto;
using Domain.Entities;
using Domain.Util;

namespace AnimalShelter.Mappers
{
    public static class MessageMapper
    {
        public static PagedResultDto<MessageDto> ToDto(this PagedResult<Message> pagedResult)
        {
            return new PagedResultDto<MessageDto>(pagedResult.Items.Select(m => m.ToDto()),
                (int)Math.Ceiling((double)pagedResult.TotalCount / pagedResult.PageSize), 
                pagedResult.Page, pagedResult.PageSize);
        }

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
