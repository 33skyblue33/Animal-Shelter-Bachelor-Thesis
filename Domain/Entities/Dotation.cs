using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Dotation
    {
        public long Id { get; set; }
        public string? Username { get; set; }
        public float Amount { get; set; }
        public string? Description { get; set; }
    }
}
