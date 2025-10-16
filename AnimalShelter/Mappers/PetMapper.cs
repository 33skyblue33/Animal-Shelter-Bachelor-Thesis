using AnimalShelter.Dto;
using Domain.Entities;

namespace AnimalShelter.Mappers
{
    public static class PetMapper
    {
        public static PetDto ToDto(this Pet pet)
        {
            return new PetDto(pet.Id, pet.Name, pet.Age, pet.Color, pet.Description, pet.ImageUrl);
        }

        public static Pet ToEntity(this PetRequest petRequest)
        {
            return new Pet()
            {
                Name = petRequest.Name,
                Age = petRequest.Age,
                Color = petRequest.Color,
                Description = petRequest.Description,
                ImageUrl = petRequest.ImageUrl,
            };
        }

        public static void Map(this Pet pet, PetRequest petRequest)
        {
            pet.Name = petRequest.Name;
            pet.Age = petRequest.Age;
            pet.Color = petRequest.Color;
            pet.Description = petRequest.Description;
            pet.ImageUrl = petRequest.ImageUrl;
        }
    }
}
