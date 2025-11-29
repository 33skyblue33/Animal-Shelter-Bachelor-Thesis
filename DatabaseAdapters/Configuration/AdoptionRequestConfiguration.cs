using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseAdapters.Configuration
{
    internal class AdoptionRequestConfiguration : IEntityTypeConfiguration<AdoptionRequest>
    {
        public void Configure(EntityTypeBuilder<AdoptionRequest> builder)
        {
            builder.ToTable("AdoptionRequests");
            builder.HasKey(ar => ar.Id);
            builder.HasOne<Pet>().WithMany()
                .HasForeignKey(ar => ar.PetId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne<User>().WithMany()
                .HasForeignKey(ar => ar.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
