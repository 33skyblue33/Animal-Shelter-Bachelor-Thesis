using AnimalShelter.Dto;
using AnimalShelter.Mappers;
using Domain.Entities;
using Domain.Ports.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DotationsControllers(IDotationService dotationService, IUnitOfWork unitOfWork) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<DotationDto>> GetById(long id)
        {
            Dotation? dotation = await dotationService.GetByIdAsync(id);
            return dotation is not null ? Ok(dotation.ToDto()) : NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<List<DotationDto>>> GetAll()
        {
            IEnumerable<Dotation> dotation = await dotationService.GetAllAsync();
            return Ok(dotation.Select(d => d.ToDto()).ToList());
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] DotationRequest dotationRequest)
        {
            Dotation dotation = dotationRequest.ToEntity();
            return await dotationService.CreateAsync(dotation) ? CreatedAtAction(nameof(GetById), new { id = dotation.Id }, dotation.ToDto()) : UnprocessableEntity();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(long id, [FromBody] DotationRequest dotationRequest)
        {
            Dotation dotation = await dotationService.GetByIdAsync(id);

            if (dotation is null)
            {
                return NotFound();
            }

            dotation.Map(dotationRequest);
            return await dotationService.SaveAsync() ? Ok() : UnprocessableEntity();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            return await dotationService.DeleteByIdAsync(id) ? NoContent() : NotFound();
        }
    }
}
