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
    internal class DotationConfiguration : IEntityTypeConfiguration<Dotation>
    {
        public void Configure(EntityTypeBuilder<Dotation> builder)
        {
            builder.ToTable("Dotations");
            builder.HasKey(d => d.Id);
        }
    }
}
