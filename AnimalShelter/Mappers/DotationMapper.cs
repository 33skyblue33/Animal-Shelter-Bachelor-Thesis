using AnimalShelter.Dto;
using Domain.Entities;

namespace AnimalShelter.Mappers
{
    public static class DotationMapper
    {
        public static DotationDto ToDto(this Dotation dotation)
        {
            return new DotationDto(dotation.Id, dotation.Username, dotation.Amount, dotation.Description);
        }

        public static Dotation ToEntity(this DotationRequest dotationRequest)
        {
            return new Dotation()
            {
                Username = dotationRequest.Username,
                Amount = dotationRequest.Amount,
                Description = dotationRequest.Description
            };
        }

        public static void Map(this Dotation dotation, DotationRequest dotationRequest)
        {
            dotation.Username = dotationRequest.Username;
            dotation.Amount = dotationRequest.Amount;
            dotation.Description = dotationRequest.Description;

        }
    }
}
