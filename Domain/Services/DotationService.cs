using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Ports.Repositories;

namespace Domain.Services
{
    public class DotationService(IDotationRepository dotationRepository, IUnitOfWork unitOfWork) : IDotationService
    {
        public async Task<bool> CreateAsync(Dotation dotation)
        {
            await dotationRepository.AddAsync(dotation);
            return await unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteByIdAsync(long id)
        {
            Dotation? dotation = await dotationRepository.GetByIdAsync(id);
            if (dotation is null) 
            {
               return false;
            }
            dotationRepository.Remove(dotation);
            return await unitOfWork.CompleteAsync() > 0;
        }

        public async Task<IEnumerable<Dotation>> GetAllAsync()
        {
            return await dotationRepository.GetAllAsync();
        }

        public async Task<Dotation> GetByIdAsync(long id)
        {
            return await dotationRepository.GetByIdAsync(id);
        }

        public async Task<bool> SaveAsync()
        {
            return await unitOfWork.CompleteAsync() > 0;
        }
    }
}
