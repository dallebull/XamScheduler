using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FriskaClient
{
    public partial class App : Application
    {
        public static bool IsUserLoggedIn { get; set; }
        public static string Auth { get; set; }
        public static string User { get; set; }
        public static bool Devmode = false;

        public static string url = "https://31.208.194.94/";
         public App()
        {


            //FriskaRöd = #502828ff
            //NyBlå = 2828ff

            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage())
                {
				
                    BarBackgroundColor = Color.FromHex("#502828ff"),
                    BarTextColor = Color.White
                };
      
            }
            else
            {
                MainPage = new NavigationPage(new FriskaClient.MainPage())
                {
				
                    BarBackgroundColor = Color.FromHex("#502828ff"),
                    BarTextColor = Color.White
                };
           
            }

        }

        protected override void OnStart()
        {
            
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
