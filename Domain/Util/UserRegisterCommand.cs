using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Util
{
    public record UserRegisterCommand(string Name, int Age, string Email, string Password);
}
