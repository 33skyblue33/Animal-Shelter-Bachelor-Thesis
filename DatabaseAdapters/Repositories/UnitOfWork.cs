using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Ports.Repositories;

namespace DatabaseAdapters.Repositories
{
    internal class UnitOfWork(IDatabaseContext databaseContext) : IUnitOfWork
    {
        public async Task<int> CompleteAsync()
        {
            return await databaseContext.SaveChangesAsync();
        }
    }
}
