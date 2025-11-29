using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Ports
{
    public interface IEmailService
    {
        Task SendVerificationEmailAsync(User user, VerificationToken token);
    }
}
