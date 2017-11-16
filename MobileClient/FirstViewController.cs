using System;
using Microsoft.AppCenter;
using UIKit;

namespace MobileClient
{
    public partial class FirstViewController : UIViewController
    {
        protected FirstViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            AppCenterLog.Info("TabLocation", "I swapped to the first tab!");

            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void UIButton219_TouchUpInside(UIButton sender)
        {
            try
            {
                throw new Exception("ERMERGER I THRER AN ERROR!!!");
            }
            catch (Exception ex)
            {
                AppCenterLog.Error("Error!", "Button of Doom was clicked");
            }
        }
    }
}
