using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Ports.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAdapters.Repositories
{
    internal class VerificationTokenRepository(IDatabaseContext databaseContext) : IVerificationTokenRepository
    {
        public async Task AddAsync(VerificationToken verificationToken)
        {
            await databaseContext.VerificationTokens.AddAsync(verificationToken);
        }

        public async Task<VerificationToken?> GetByTokenAsync(string token)
        {
            return await databaseContext.VerificationTokens.FirstOrDefaultAsync(v => v.Revoked == null && v.Token == token);
        }
    }
}
