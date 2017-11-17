using System;
using UIKit;
using Microsoft.AppCenter;
using IdentityModel.OidcClient;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text;

namespace MobileClient
{
    public partial class SecondViewController : UIViewController
    {
        OidcClient _client;
        LoginResult _result;

        protected SecondViewController(IntPtr handle) : base(handle)
        {
            var options = new OidcClientOptions
            {
                Authority = "https://demo.identityserver.io",
                ClientId = "native.hybrid",
                Scope = "openid profile email api",
                RedirectUri = "SFAuthenticationSessionExample://callback",
                Browser = new SystemBrowser(),

                ResponseMode = OidcClientOptions.AuthorizeResponseMode.Redirect
            };

            _client = new OidcClient(options);
        }

        public override void ViewDidLoad()
        {
            AppCenterLog.Verbose("TabLocation", "I swapped to the second tab!");

            base.ViewDidLoad();

            LoginButton.TouchUpInside += LoginButton_TouchUpInside;
            CallApiButton.TouchUpInside += CallApiButton_TouchUpInside;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        async void LoginButton_TouchUpInside(object sender, EventArgs e)
        {
            _result = await _client.LoginAsync(new LoginRequest());

            if (_result.IsError)
            {
                OutputText.Text = _result.Error;
                return;
            }

            var sb = new StringBuilder(128);
            foreach (var claim in _result.User.Claims)
            {
                sb.AppendFormat("{0}: {1}\n", claim.Type, claim.Value);
            }

            sb.AppendFormat("\n{0}: {1}\n", "refresh token", _result?.RefreshToken ?? "none");
            sb.AppendFormat("\n{0}: {1}\n", "access token", _result.AccessToken);

            OutputText.Text = sb.ToString();
        }

        async void CallApiButton_TouchUpInside(object sender, EventArgs e)
        {
            if (_result?.AccessToken != null)
            {
                var client = new HttpClient();
                client.SetBearerToken(_result.AccessToken);

                var response = await client.GetAsync("https://demo.identityserver.io/api/test");
                if (!response.IsSuccessStatusCode)
                {
                    OutputText.Text = response.ReasonPhrase;
                    return;
                }

                var content = await response.Content.ReadAsStringAsync();
                OutputText.Text = JArray.Parse(content).ToString();
            }
        }
    }
}
