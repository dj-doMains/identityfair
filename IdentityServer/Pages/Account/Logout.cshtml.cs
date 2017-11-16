using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using IdentityServer.Data;
using IdentityServer4.Services;

namespace IdentityServer.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IIdentityServerInteractionService _interaction;

        public LogoutModel(SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger,
            IIdentityServerInteractionService interaction)
        {
            _signInManager = signInManager;
            _logger = logger;
            _interaction = interaction;
        }

        public string ReturnUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string logoutId = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            var context = await _interaction.GetLogoutContextAsync(logoutId);

            if (string.IsNullOrEmpty(context?.PostLogoutRedirectUri))
                return RedirectToPage("/Index");
            else
                return Redirect(context.PostLogoutRedirectUri);
        }
    }
}
