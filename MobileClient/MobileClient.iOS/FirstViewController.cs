using System;
using System.Linq;
using Foundation;
using Microsoft.AppCenter;
using MobileClient.Shared.Identity;
using UIKit;
using Xamarin.Auth;

namespace MobileClient
{
    public partial class FirstViewController : UIViewController
    {
        public SecurityToken SecurityToken { get; set; }

        protected FirstViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            AppCenterLog.Info("TabLocation", "I swapped to the first tab!");

            // temporary solution until I can get Accounts working
            UsernameTextBox.Text = NSUserDefaults.StandardUserDefaults.StringForKey("Username");
            AccessTokenTextView.Text = NSUserDefaults.StandardUserDefaults.StringForKey("AccessToken");
            RefreshTokenTextBox.Text = NSUserDefaults.StandardUserDefaults.StringForKey("RefreshToken");
            TokenTypeTextBox.Text = NSUserDefaults.StandardUserDefaults.StringForKey("TokenType");
            ExpiresInTextBox.Text = NSUserDefaults.StandardUserDefaults.StringForKey("ExpiresIn");
            ExpirationDateTimeTextBox.Text = NSUserDefaults.StandardUserDefaults.StringForKey("ExpirationDate");

            //var account = AccountStore.Create().FindAccountsForService("com.neekydomains.MobileClient").FirstOrDefault();

            //if (account != null)
            //{
            //    UsernameTextBox.Text = account.Username;
            //    AccessTokenTextView.Text = account.Properties["AccessToken"];
            //    RefreshTokenTextBox.Text = account.Properties["RefreshToken"];
            //    TokenTypeTextBox.Text = account.Properties["TokenType"];
            //    ExpiresInTextBox.Text = account.Properties["ExpiresIn"];
            //    ExpirationDateTimeTextBox.Text = account.Properties["ExpirationDate"];
            //}

            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void ButtonOfDoom_TouchUpInside(UIButton sender)
        {
            try
            {
                throw new Exception("ERMERGER I THRER AN ERROR!!!");
            }
            catch (Exception ex)
            {
                AppCenterLog.Error("Error!", $"Button of Doom was clicked - Error:{ex.Message}");
            }
        }
    }
}
