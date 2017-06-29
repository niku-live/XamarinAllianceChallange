
using Foundation;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Threading.Tasks;
using UIKit;
using XamarinAllianceApp.Controllers;

namespace XamarinAllianceApp.iOS
{
    [Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IAuthenticate
    {
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// Initialize Xamarin Forms
			global::Xamarin.Forms.Forms.Init ();
            App.Init(this);
            LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}

        // Define a authenticated user.
        private MobileServiceUser user;

        public async Task<bool> Authenticate()
        {
            var success = false;
            var message = string.Empty;
            try
            {
                // Sign in with Facebook login using a server-managed flow.
                if (user == null)
                {
                    user = await XamarinAllianceApp.Controllers.CharacterService.DefaultManager.CurrentClient
                        .LoginAsync(UIApplication.SharedApplication.KeyWindow.RootViewController,
                        MobileServiceAuthenticationProvider.MicrosoftAccount);
                    if (user != null)
                    {
                        message = string.Format("You are now signed-in as {0}.", user.UserId);
                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            // Display the success or failure message.
            UIAlertView avAlert = new UIAlertView("Sign-in result", message, null, "OK", null);
            avAlert.Show();

            return success;
        }
    }
}

