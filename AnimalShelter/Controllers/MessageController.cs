using AnimalShelter.Dto;
using AnimalShelter.Mappers;
using Domain.Entities;
using Domain.Services;
using Domain.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController(IMessageService messageService) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageDto>> GetById(long id)
        {
            Message? message = await messageService.GetByIdAsync(id);

            return message is not null ? Ok(message.ToDto()) : NotFound();
        }

        [HttpGet("{conversationId}/history")]
        public async Task<ActionResult<PagedResultDto<MessageDto>>> GetPagedByConversationId(long conversationId, int page, int pageSize)
        {
            PagedResult<Message> pagedResult = await messageService.GetPagedByConversationIdAsync(conversationId, page, pageSize);
            return Ok(pagedResult.ToDto());
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] MessageRequest messageRequest)
        {
            Message message = messageRequest.ToEntity();
            return await messageService.CreateAsync(message) ? CreatedAtAction(nameof(GetById), new { id = message.Id }, message.ToDto()) : UnprocessableEntity();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(long id, [FromBody] MessageRequest messageRequest)
        {
            Message? message = await messageService.GetByIdAsync(id);

            if (message is null)
            {
                return NotFound();
            }

            message.Map(messageRequest);
            return await messageService.SaveAsync() ? Ok() : UnprocessableEntity();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            return await messageService.DeleteAsync(id) ? NoContent() : NotFound();
        }

    }
}
