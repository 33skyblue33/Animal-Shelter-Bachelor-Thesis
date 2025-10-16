using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Ports.Repositories;

namespace Domain.Services
{
    public class PetService(IPetRepository petRepository, IUnitOfWork unitOfWork) : IPetService
    {
        public async Task<bool> CreateAsync(Pet pet)
        {
            await petRepository.AddAsync(pet);
            return await unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteByIdAsync(long id)
        {
            Pet? pet = await petRepository.GetByIdAsync(id);

            if(pet is null)
            {
                return false;
            }

            petRepository.Remove(pet);
            return await unitOfWork.CompleteAsync() > 0;
        }

        public async Task<IEnumerable<Pet>> GetAllAsync()
        {
            return await petRepository.GetAllAsync();
        }

        public async Task<Pet?> GetByIdAsync(long id)
        {
            return await petRepository.GetByIdAsync(id);
        }

        public async Task<bool> SaveAsync()
        {
            return await unitOfWork.CompleteAsync() > 0;
        }
    }
}
