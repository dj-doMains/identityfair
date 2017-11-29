using Foundation;
using System;
using UIKit;
using MobileClient.Shared.Identity;
using Xamarin.Auth;
using MobileClient.Models.Identity;

namespace MobileClient
{
    public partial class LoginPageViewController : UIViewController
    {
        public LoginPageViewController(IntPtr handle) : base(handle)
        {
        }

        partial void LoginButton_TouchUpInside(UIButton sender)
        {
            bool isValidUser = ValidateUser(UserNameTextField.Text, PasswordTextField.Text);

            if (isValidUser)
            {
                var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;

                var tabBarController = appDelegate.GetViewController(appDelegate.MainStoryboard, "MainTabBarController");
                appDelegate.SetRootViewController(tabBarController, true);
            }
            else
            {
                IUIAlertViewDelegate del = null;
                new UIAlertView("Login Error", "Bad user name or password", del, "OK", null).Show();
            }
        }

        private bool ValidateUser(string username, string password)
        {
            IdentityManager identityManager = new IdentityManager(IdentityHelper.CreateIdentityManagerSettings());
            var token = identityManager.SignIn(username, password);

            if (!string.IsNullOrEmpty(token?.AccessToken?.Trim()))
            {
                // temporary solution until I can get Accounts working
                NSUserDefaults.StandardUserDefaults.SetString(username, "Username");
                NSUserDefaults.StandardUserDefaults.SetString(token.AccessToken, "AccessToken");
                NSUserDefaults.StandardUserDefaults.SetString(token.TokenType, "TokenType");
                NSUserDefaults.StandardUserDefaults.SetString(token.RefreshToken, "RefreshToken");
                NSUserDefaults.StandardUserDefaults.SetString(token.ExpiresIn.ToString(), "ExpiresIn");
                NSUserDefaults.StandardUserDefaults.SetString(token.AccessToken, "ExpirationDate");

                NSUserDefaults.StandardUserDefaults.Synchronize();

                //Account account = new Account(username);

                //account.Properties.Add("AccessToken", token.AccessToken);
                //account.Properties.Add("TokenType", token.TokenType);
                //account.Properties.Add("RefreshToken", token.RefreshToken);
                //account.Properties.Add("ExpiresIn", token.ExpiresIn.ToString());
                //account.Properties.Add("ExpirationDate", token.ExpirationDate.ToString());

                //AccountStore.Create().Save(account, "MobileClient_Security");

                return true;
            }

            return false;
        }
    }
}