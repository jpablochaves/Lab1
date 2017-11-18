using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Lab1.View;

namespace Lab1
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            NavigationPage navigation = new NavigationPage(new MainPage());

            App.Current.MainPage = new MasterDetailPage
            {
                Master = new HomeMenu(),
                Detail = navigation
            };
                        
          //   MainPage = new MainPage();
          // MainPage = new Lab1.View.FormPage();
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
	}
}
