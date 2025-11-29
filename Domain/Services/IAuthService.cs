using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Util;

namespace Domain.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(UserRegisterCommand command);
        Task VerifyAsync(string token);
        Task<AuthResult> LoginAsync(string email, string password);
        Task<AuthResult> RefreshAsync(string token);
        Task LogoutAsync(string token);
    }
}
