using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SaasKit.Multitenancy;

public class AppTenantResolver : ITenantResolver<AppTenant>
{
    IEnumerable<AppTenant> tenants = new List<AppTenant>(new[]
    {
        new AppTenant {
            Name = "Tenant 1",
            Hostnames = new[] { "localhost:6001", "localhost:6003" }
        },
        new AppTenant {
            Name = "Tenant 2",
            Hostnames = new[] { "localhost:6002" }
        }
    });

    public async Task<TenantContext<AppTenant>> ResolveAsync(HttpContext context)
    {
        TenantContext<AppTenant> tenantContext = null;

        var tenant = tenants.FirstOrDefault(t =>
            t.Hostnames.Any(h => h.Equals(context.Request.Host.Value.ToLower())));

        if (tenant != null)
        {
            tenantContext = new TenantContext<AppTenant>(tenant);
        }

        return tenantContext;
    }
}