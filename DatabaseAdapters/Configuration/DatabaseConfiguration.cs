using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseAdapters.Repositories;
using DatabaseAdapters.Sqlite;
using Domain.Ports.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseAdapters.Configuration
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection AddSqliteDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SqliteDatabaseContext>(options => options.UseSqlite(configuration.GetConnectionString("Dev")));
            services.AddScoped<IDatabaseContext, SqliteDatabaseContext>();
            return AddRepositories(services);
        }

        private static IServiceCollection AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IBreedRepository, BreedRepository>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IConversationRepository, ConversationRepository>();
            services.AddScoped<IAdoptionRequestRepository, AdoptionRequestRepository>();
            services.AddScoped<IDotationRepository, DotationRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IVerificationTokenRepository, VerificationTokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
