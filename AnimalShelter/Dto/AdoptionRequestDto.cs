using Domain.Util;

namespace AnimalShelter.Dto
{
    public record AdoptionRequestDto(long Id, long UserId, long PetId, AdoptionRequestStatus Status,  DateTime RequestDate, DateTime? ResolvedDate);
}
