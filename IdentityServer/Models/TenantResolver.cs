namespace IdentityServer.Models
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using SaasKit.Multitenancy;

    public class TenantResolver : ITenantResolver<Tenant>
    {
        private readonly Dictionary<string, string> mappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "localhost:5000", "Tenant 1"},
            { "localhost:5001", "Tenant 2"},
            { "localhost:5002", "Tenant 3"},
        };

        public Task<TenantContext<Tenant>> ResolveAsync(HttpContext context)
        {
            string tenantName = null;
            TenantContext<Tenant> tenantContext = null;

            if (mappings.TryGetValue(context.Request.Host.Value, out tenantName))
            {
                tenantContext = new TenantContext<Tenant>(
                    new Tenant { Name = tenantName, Hostnames = new[] { context.Request.Host.Value } });

                tenantContext.Properties.Add("Created", DateTime.UtcNow);
            }

            return Task.FromResult(tenantContext);
        }
    }
}