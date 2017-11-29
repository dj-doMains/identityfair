namespace IdentityServer.Models
{
    public class Tenant
    {
        public string Name { get;set ; }
        public string[] Hostnames { get; set; }
    }
}