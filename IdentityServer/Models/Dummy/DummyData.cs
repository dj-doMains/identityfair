using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Models.Dummy
{
    public static class DummyData
    {
        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "2b11357c-668a-4627-b956-c9ad1365c8b3",
                ClientName = "WebClient",

                FrontChannelLogoutUri = "http://localhost.5002/signout-oidc",

                RedirectUris = new List<string>()
                {
                    "http://localhost:5002/signin-oidc"
                },

                PostLogoutRedirectUris = new List<string>()
                {
                     "http://localhost:5002/signout-callback-oidc"
                },

                AllowedGrantTypes = GrantTypes.Hybrid,
                    
                // secret for authentication
                ClientSecrets =
                {
                    new Secret("bbda4042-7d7a-4e41-9d8e-27c3ab4a8260".Sha256())
                },

                // scopes that client has access to
                AllowedScopes = { "webapi", "openid", "profile" },

                RequireConsent = false
            }
        };

        public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>
        {
            new ApiResource("webapi", "WebApi")
        };

        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

        public static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in Clients)
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in IdentityResources)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in ApiResources)
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}