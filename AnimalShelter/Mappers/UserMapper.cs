using AnimalShelter.Dto;
using Domain.Entities;
using Domain.Util;

namespace AnimalShelter.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(this User user)
        {
            return new UserDto(user.Id, user.Name, user.Age, user.Email, user.Role.ToString());
        }

        public static UserUpdateCommand ToCommand(this UserRequest request)
        {
            return new UserUpdateCommand(request.Name, request.Age, request.Email, request.Password);
        }
    }
}
