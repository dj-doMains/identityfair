// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace MobileClient
{
    [Register ("FirstViewController")]
    partial class FirstViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView AccessTokenTextView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonOfDoom { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField ExpirationDateTimeTextBox { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField ExpiresInTextBox { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField RefreshTokenTextBox { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TokenTypeTextBox { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel UsernameTextBox { get; set; }

        [Action ("ButtonOfDoom_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ButtonOfDoom_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (AccessTokenTextView != null) {
                AccessTokenTextView.Dispose ();
                AccessTokenTextView = null;
            }

            if (ButtonOfDoom != null) {
                ButtonOfDoom.Dispose ();
                ButtonOfDoom = null;
            }

            if (ExpirationDateTimeTextBox != null) {
                ExpirationDateTimeTextBox.Dispose ();
                ExpirationDateTimeTextBox = null;
            }

            if (ExpiresInTextBox != null) {
                ExpiresInTextBox.Dispose ();
                ExpiresInTextBox = null;
            }

            if (RefreshTokenTextBox != null) {
                RefreshTokenTextBox.Dispose ();
                RefreshTokenTextBox = null;
            }

            if (TokenTypeTextBox != null) {
                TokenTypeTextBox.Dispose ();
                TokenTypeTextBox = null;
            }

            if (UsernameTextBox != null) {
                UsernameTextBox.Dispose ();
                UsernameTextBox = null;
            }
        }
    }
}