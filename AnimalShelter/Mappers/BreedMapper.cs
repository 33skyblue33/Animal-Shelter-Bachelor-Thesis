using AnimalShelter.Dto;
using Domain.Entities;

namespace AnimalShelter.Mappers
{
    public static class BreedMapper
    {
        public static BreedDto ToDto(this Breed breed)
        {
            return new BreedDto(breed.Id, breed.Name, breed.Description);
        }

        public static Breed ToEntity(this BreedRequest breedRequest)
        {
            return new Breed()
            {
                Name = breedRequest.Name,
                Description = breedRequest.Description
            };
        }

        public static void Map(this Breed breed, BreedRequest breedRequest)
        {
            breed.Name = breedRequest.Name;
            breed.Description = breedRequest.Description;
        }

    }
}
