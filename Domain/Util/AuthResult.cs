using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Util
{
    public record AuthResult(string AccessToken, RefreshToken RefreshToken, User User);
}
