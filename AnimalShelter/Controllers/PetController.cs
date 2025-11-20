using System.Reflection.Metadata;
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
    public class PetController(IPetService petService) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<PetDto>> GetById(long id)
        {
            Pet? pet = await petService.GetByIdAsync(id);

            return pet is not null ? Ok(pet.ToDto()) : NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<List<PetDto>>> GetAll()
        {
            IEnumerable<Pet> pets = await petService.GetAllAsync();
            return Ok(pets.Select(p => p.ToDto()).ToList());
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] PetRequest petRequest)
        {
            Pet pet = petRequest.ToEntity();
            return await petService.CreateAsync(pet) ? CreatedAtAction(nameof(GetById), new { id = pet.Id }, pet.ToDto()) : UnprocessableEntity();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(long id, [FromBody] PetRequest petRequest)
        {
            Pet? pet = await petService.GetByIdAsync(id);

            if (pet is null)
            {
                return NotFound();
            }

            pet.Map(petRequest);
            return await petService.SaveAsync() ? Ok() : UnprocessableEntity();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            return await petService.DeleteByIdAsync(id) ? NoContent() : NotFound();

        }
    }
    }
