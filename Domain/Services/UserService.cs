using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Ports;
using Domain.Ports.Repositories;
using Domain.Util;

namespace Domain.Services
{
    public class UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork) : IUserService
    {
        public async Task<User?> CreateAsync(User user)
        {
            await userRepository.AddAsync(user);
            return user;
        }

        public async Task<bool> DeleteAllInactiveAsync()
        {
            IEnumerable<User> inactiveUsers = await userRepository.GetAllInactiveAsync();
            foreach (User user in inactiveUsers)
            {
                userRepository.Remove(user);
            }
            return await unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteByIdAsync(long id)
        {
            User? userToDelete = await userRepository.GetByIdAsync(id);
            if(userToDelete == null)
            {
                return false;
            }

            userRepository.Remove(userToDelete);
            return await unitOfWork.CompleteAsync() > 0;
        }

        public async Task<User?> GetByIdAsync(long id)
        {
           return await userRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateByIdAsync(long id, UserUpdateCommand data)
        {
            User? foundUser = await userRepository.GetByIdAsync(id);
             
            if (foundUser == null)
            {
                return false;
            }

            foundUser.Name = data.Name;
            foundUser.Age = data.Age;
            foundUser.Email = data.Email;
            foundUser.PasswordHash = passwordHasher.Hash(data.Password);

            return await unitOfWork.CompleteAsync() > 0;
        }
    }
}
