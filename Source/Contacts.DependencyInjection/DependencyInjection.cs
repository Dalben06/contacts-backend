using Contacts.Auth.Application.Services;
using Contacts.Auth.Data.Repositories;
using Contacts.Auth.Domain.IRepositories;
using Contacts.Contact.Application.Services;
using Contacts.Contact.Data.Repositories;
using Contacts.Contact.Domain.IRepositories;
using Contacts.Core.Configuration;
using Contacts.Core.Data;
using Contacts.DependencyInjection.Setup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contacts.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<Settings>(x => configuration.Get<Settings>());
            services.RegisterConfig();
            services.RegisterCore();
            services.RegisterContacts();
            services.RegisterAuth();
        }

        private static void RegisterConfig(this IServiceCollection services)
        {
            services.AddDapperConfig();
            services.AddAutoMapperConfiguration();
        }
        private static void RegisterCore(this IServiceCollection services)
        {
            services.AddScoped<DbContext>();
            services.AddScoped<DbFactory>();
        }

        private static void RegisterContacts(this IServiceCollection services)
        {
            services.AddScoped<IPersonAppService, PersonAppService>();
            services.AddScoped<IPersonRepository, PersonRepository>();
        }

        private static void RegisterAuth(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
        }
    }
}
