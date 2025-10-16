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
    public class BreedsController(BreedService breedService) : ControllerBase
    {
        [HttpGet("{id}")] 
        public async Task<ActionResult<BreedDto>> GetById(long id)
        {
            Breed? breed = await breedService.GetByIdAsync(id);

            return breed is not null ? Ok(breed.ToDto()) : NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<List<BreedDto>>> GetAll()
        {
            IEnumerable<Breed> breeds = await breedService.GetAllAsync();
            return Ok(breeds.Select(b => b.ToDto()).ToList());
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] BreedRequest breedRequest)
        {
            Breed breed = breedRequest.ToEntity();
            return await breedService.CreateAsync(breed) ? CreatedAtAction(nameof(GetById), new {id =  breed.Id}, breed.ToDto()) : UnprocessableEntity();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(long id, [FromBody] BreedRequest breedRequest)
        {
            Breed? breed = await breedService.GetByIdAsync(id);
            
            if(breed is null)
            {
                return NotFound();
            }

            breed.Map(breedRequest);
            return await breedService.SaveAsync() ? Ok() : UnprocessableEntity();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
           return await breedService.DeleteByIdAsync(id) ? NoContent() : NotFound();

        }

    }
}
