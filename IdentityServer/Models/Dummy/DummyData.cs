using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using IdentityServer.Data;
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
            },
            new Client
            {
                ClientId = "494d4635-e87d-4c1f-9f49-b539373f5af6",
                ClientName = "MobileClient",

                AccessTokenLifetime = 144000, // 4 hours

                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedScopes = { "webapi" },
                AllowOfflineAccess = true,

                ClientSecrets =
                {
                    new Secret("28a0a69a-0711-479c-97b3-26c24c47c3b2".Sha256())
                },

                RequireConsent = false,
                RequireClientSecret = false
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

        public static void InitializeDatabase(IApplicationBuilder app, bool forceRefresh = false)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var configContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                if (forceRefresh)
                {
                    var pgContext = serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
                    var appContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    appContext.Database.EnsureDeleted();
                    appContext.Database.EnsureCreated();

                    configContext.Database.Migrate();
                    pgContext.Database.Migrate();
                }

                if (!configContext.Clients.Any())
                {
                    foreach (var client in Clients)
                    {
                        configContext.Clients.Add(client.ToEntity());
                    }
                    configContext.SaveChanges();
                }

                if (!configContext.IdentityResources.Any())
                {
                    foreach (var resource in IdentityResources)
                    {
                        configContext.IdentityResources.Add(resource.ToEntity());
                    }
                    configContext.SaveChanges();
                }

                if (!configContext.ApiResources.Any())
                {
                    foreach (var resource in ApiResources)
                    {
                        configContext.ApiResources.Add(resource.ToEntity());
                    }
                    configContext.SaveChanges();
                }
            }
        }
    }
}