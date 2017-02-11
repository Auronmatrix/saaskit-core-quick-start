using System.Collections.ObjectModel;

namespace saaskit.Models{

public class MultitenancyOptions
    {
        public Collection<AppTenant> Tenants { get; set; }
    
    }
}