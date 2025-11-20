using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Pet
    {
        public long Id { get; set; }
        public long BreedId { get; set; }
        public required string Name { get; set; }
        public required string Age { get; set; }
        public required string Color { get; set; }
        public required string Description { get; set; }
        public required string ImageUrl { get; set; }

    }
}
