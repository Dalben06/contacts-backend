using Contacts.Auth.Application.AutoMapper;
using Contacts.Contact.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Contacts.DependencyInjection.Setup
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(ContactProfile));
            services.AddAutoMapper(typeof(AuthProfile));
        }
    }
}
