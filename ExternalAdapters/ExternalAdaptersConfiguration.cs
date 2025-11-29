using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace ExternalAdapters
{
    public static class ExternalAdaptersConfiguration
    {
        public static IServiceCollection AddExternalAdapters(this IServiceCollection services)
        {
            services.AddSingleton<IEmailService, GmailEmailService>();
            services.AddSingleton<IPasswordHasher, ArgonPasswordHasher>();
            services.AddSingleton<ITokenGenerator, JwtTokenGenerator>();

            return services;
        }
    }
}
