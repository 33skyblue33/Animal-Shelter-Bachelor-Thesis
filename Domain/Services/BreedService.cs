using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Ports.Repositories;

namespace Domain.Services
{
    public class BreedService(IBreedRepository breedRepository, IUnitOfWork unitOfWork) : IBreedService
    {
        public async Task<bool> CreateAsync(Breed breed)
        {
            await breedRepository.AddAsync(breed);
            return await unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteByIdAsync(long id)
        {
            Breed? breed = await breedRepository.GetByIdAsync(id);

            if(breed is null)
            {
                return false;
            }

            breedRepository.Remove(breed);
            return await unitOfWork.CompleteAsync() > 0;
        }

        public async Task<IEnumerable<Breed>> GetAllAsync()
        {
            return await breedRepository.GetAllAsync();
        }

        public async Task<Breed?> GetByIdAsync(long id)
        {
            return await breedRepository.GetByIdAsync(id);
        }

        public async Task<bool> SaveAsync()
        {
            return await unitOfWork.CompleteAsync() > 0;
        }
    }
}
