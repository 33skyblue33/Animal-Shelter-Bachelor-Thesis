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
    public class AdoptionRequestController(AdoptionRequestService adoptionRequestService) : ControllerBase
    {
        [HttpGet("{id}")] 
        public async Task<ActionResult<AdoptionRequestDto>> GetById(long id)
        {
            AdoptionRequest? adoptionRequest = await adoptionRequestService.GetByIdAsync(id);
            return adoptionRequest is null ? Ok(adoptionRequest.ToDto()) : NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<List<AdoptionRequestDto>>> GetAll()
        {
            IEnumerable<AdoptionRequest> adoptionRequests = await adoptionRequestService.GetAllAsync();
            return Ok(adoptionRequests.Select(a => a.ToDto()).ToList());
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] AdoptionRequestRequest adoptionRequestRequest)
        {
            AdoptionRequest adoptionRequest = adoptionRequestRequest.ToEntity();
            return await adoptionRequestService.CreateAsync(adoptionRequest) ? CreatedAtAction(nameof(GetById), new { id = adoptionRequest.Id }, adoptionRequest.ToDto()) : UnprocessableEntity();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(long id, [FromBody] AdoptionRequestRequest adoptionRequestRequest)
        {
            AdoptionRequest? adoptionRequest = await adoptionRequestService.GetByIdAsync(id);

            if (adoptionRequest is null)
            {
                return NotFound();
            }

            adoptionRequest.Map(adoptionRequestRequest);
            return await adoptionRequestService.UpdateAsync(id, adoptionRequest) ? Ok() : UnprocessableEntity();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            return await adoptionRequestService.DeleteAsync(id) ? NoContent() : NotFound();

        }

    }
}
