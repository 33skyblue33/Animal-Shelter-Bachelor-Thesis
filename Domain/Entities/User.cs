using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Util;

namespace Domain.Entities
{
    public class User
    {
        public long Id { get; set; }
        public required string Name {  get; set; }
        public int Age { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public UserRole Role { get; set; }
    }
}
