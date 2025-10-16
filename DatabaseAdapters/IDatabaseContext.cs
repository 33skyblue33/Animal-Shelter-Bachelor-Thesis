using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAdapters
{
    internal interface IDatabaseContext
    {
        public DbSet<Breed> Breeds { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Dotation> Dotations { get; set; }
        public DbSet<AdoptionRequest> AdoptionRequests { get; set; }
        object Pet { get; }

        Task<int> SaveChangesAsync();

        
        
    }
}
