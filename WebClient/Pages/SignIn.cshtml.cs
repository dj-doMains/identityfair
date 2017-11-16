using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebClient.Pages
{
    [Authorize]
    public class SignInModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            await Task.CompletedTask;

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPost()
        {
            await Task.CompletedTask;

            return RedirectToPage("Index");
        }
    }
}