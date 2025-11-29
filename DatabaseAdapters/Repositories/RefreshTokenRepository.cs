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
    internal class RefreshTokenRepository(IDatabaseContext databaseContext) : IRefreshTokenRepository
    {
        public async Task AddAsync(RefreshToken refreshToken)
        {
            await databaseContext.RefreshTokens.AddAsync(refreshToken);
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await databaseContext.RefreshTokens.FirstOrDefaultAsync(r => r.Token == token && r.Revoked == null && r.Expires > DateTime.UtcNow);
        }
    }
}
