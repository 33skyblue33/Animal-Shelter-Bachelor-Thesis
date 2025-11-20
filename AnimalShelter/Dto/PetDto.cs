namespace AnimalShelter.Dto
{
    public record PetDto(long Id, long BreedId, string Name, string Age, string Color, string Description, string ImageUrl);
}
