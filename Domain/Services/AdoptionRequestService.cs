using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Ports.Repositories;

namespace Domain.Services
{
    public class AdoptionRequestService(IUnitOfWork unitOfWork, IAdoptionRequestRepository adoptionRequestRepository) : IAdoptionRequestService
    {
        public async Task<bool> CreateAsync(AdoptionRequest adoptionRequest)
        {
            await adoptionRequestRepository.AddAsync(adoptionRequest);
            return await unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            AdoptionRequest? adoptionRequest =await adoptionRequestRepository.GetByIdAsync(id);
            if (adoptionRequest != null)
            {
                return false;
            }

            return await unitOfWork.CompleteAsync() > 0;
        }

        public async Task<IEnumerable<AdoptionRequest>> GetAllAsync()
        {
            return await adoptionRequestRepository.GetAllAsync();
        }

        public async Task<AdoptionRequest?> GetByIdAsync(long id)
        {
            return await adoptionRequestRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(long id, AdoptionRequest adoptionRequest)
        {
            await adoptionRequestRepository.GetAllAsync();
            return await  unitOfWork.CompleteAsync() > 0;
        }

        
    }
}
