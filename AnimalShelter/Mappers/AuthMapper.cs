using AnimalShelter.Dto;
using Domain.Util;

namespace AnimalShelter.Mappers
{
    public static class AuthMapper
    {
        public static AuthResultDto ToDto(this AuthResult result)
        {
            return new AuthResultDto(result.User.Id, result.AccessToken);
        }
    }
}
