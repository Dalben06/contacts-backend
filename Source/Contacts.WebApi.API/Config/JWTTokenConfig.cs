using Contacts.Auth.Application.Services;
using Contacts.Core.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Contacts.WebApi.API.Config
{
    public static class JWTTokenConfig
    {
        public static void AddJWTConfiguration(this IServiceCollection services, Settings settings)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(x =>
             {
                 x.Events = new JwtBearerEvents
                 {
                     OnTokenValidated = async context =>
                     {
                         if (!context.Principal.Identity.IsAuthenticated)
                             context.Fail("Unauthorized");

                         var claim = context.Principal.FindFirst(ClaimTypes.NameIdentifier);
                         if (claim == null)
                             context.Fail("Unauthorized");

                         var autenticacaoService = context.HttpContext.RequestServices.GetRequiredService<IAuthService>();

                         if (!(await autenticacaoService.IsUserAutheticated(new Guid(claim?.Value ?? Guid.Empty.ToString()))))
                             context.Fail("Unauthorized");

                     }
                 };
                 x.RequireHttpsMetadata = false;
                 x.SaveToken = true;
                 x.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.Configuration.Secret)),
                     ValidateIssuer = false,
                     ValidateAudience = false
                 };
             });


            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
        }
    }
}
