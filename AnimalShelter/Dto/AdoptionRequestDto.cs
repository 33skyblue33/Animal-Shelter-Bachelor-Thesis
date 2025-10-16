using Domain.Util;

namespace AnimalShelter.Dto
{
    public record AdoptionRequestDto(long Id, AdoptionRequestStatus Status,  DateTime RequestDate, DateTime? ResolvedDate);
}
