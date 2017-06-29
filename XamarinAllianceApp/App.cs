using System;

using Xamarin.Forms;
using XamarinAllianceApp.Controllers;
using XamarinAllianceApp.Views;

namespace XamarinAllianceApp
{
	public class App : Application
	{
		public App ()
		{
            // The root page of your application
            MainPage = new MainMasterDetailPage();//new CharacterListPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        public static IAuthenticate Authenticator { get; private set; }

        public static void Init(IAuthenticate authenticator)
        {
            Authenticator = authenticator;
        }
    }
}

