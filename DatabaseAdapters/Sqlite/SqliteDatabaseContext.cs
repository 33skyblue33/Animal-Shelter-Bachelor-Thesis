using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAdapters.Sqlite
{
    internal class SqliteDatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<Breed> Breeds { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Dotation> Dotations { get; set; }
        public DbSet<AdoptionRequest> AdoptionRequests { get; set; }

        public object Pet => throw new NotImplementedException();

        public SqliteDatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
