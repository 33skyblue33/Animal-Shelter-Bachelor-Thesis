using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Ports.Repositories
{
    public interface IVerificationTokenRepository
    {
        Task<VerificationToken?> GetByTokenAsync(string token);
        Task AddAsync(VerificationToken verificationToken);
    }
}
