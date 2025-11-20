namespace AnimalShelter.Dto
{
    public record PetRequest(long BreedId, string Name, string Age, string Color, string Description, string ImageUrl);
}
