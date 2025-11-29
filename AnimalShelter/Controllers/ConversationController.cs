using AnimalShelter.Dto;
using AnimalShelter.Mappers;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController(IConversationService conversationService) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ConversationDto>> GetById(long id)
        {
           Conversation? conversation = await conversationService.GetByIdAsync(id);
           return conversation != null ? Ok(conversation.ToDto()) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ConversationRequest request)
        {
            Conversation conversation = ConversationMapper.ToEntity(request);
            return await conversationService.AddAsync(conversation) ? CreatedAtAction(nameof(GetById), new { id = conversation.Id }, conversation.ToDto()) : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            return await conversationService.DeleteAsync(id) ? NoContent() : NotFound();
        }
    }
}
