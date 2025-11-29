using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Ports
{
    public interface ITokenGenerator
    {
        string GenerateAccessToken(User user);
        RefreshToken GenerateRefreshToken(User user);
        VerificationToken GenerateVerificationToken(User user);
    }
}
