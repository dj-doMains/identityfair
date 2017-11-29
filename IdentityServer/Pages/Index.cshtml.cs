using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IdentityServer.Models;

namespace IdentityServer.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(Tenant tenant)
        {
            this.Tenant = tenant;
        }

        public Tenant Tenant { get; set; }

        public void OnGet()
        {

        }
    }
}
