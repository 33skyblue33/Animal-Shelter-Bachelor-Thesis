using Domain.Util;

namespace AnimalShelter.Dto
{
    public record AdoptionRequestRequest(long UserId, long PetId);
}
