using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebClient.Pages
{
    public class SuperSecretModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Questionable";
        }
    }
}
