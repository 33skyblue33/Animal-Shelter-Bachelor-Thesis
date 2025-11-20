using AnimalShelter.Dto;
using Domain.Entities;
using Domain.Util;

namespace AnimalShelter.Mappers
{
    public static class AdoptionRequestMapper
    {
        public static AdoptionRequestDto ToDto ( this AdoptionRequest adoptionRequest)
        {
            return new AdoptionRequestDto(adoptionRequest.Id, adoptionRequest.UserId, adoptionRequest.PetId, adoptionRequest.Status, adoptionRequest.RequestDate, adoptionRequest.ResolvedDate);   
        }

        public static AdoptionRequest ToEntity( this AdoptionRequestRequest adoptionRequestRequest )
        {
            return new AdoptionRequest()
            {
                UserId = adoptionRequestRequest.UserId,
                PetId = adoptionRequestRequest.PetId,
                Status = AdoptionRequestStatus.InProgress,
                RequestDate = DateTime.UtcNow,
                ResolvedDate = null
            };

        }

        public static void Map(this AdoptionRequest adoptionRequest, AdoptionRequestRequest adoptionRequestRequest)
        {
            adoptionRequest.UserId = adoptionRequestRequest.UserId;
            adoptionRequest.PetId = adoptionRequestRequest.PetId;
        }

    }
}
