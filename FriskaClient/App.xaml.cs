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
        public  static bool Devmode { get; set; }
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTUzMzg2QDMxMzcyZTMzMmUzMFloS2I3ZzhtTUpObzRhbzhraTR3RWQ1WlBESFhLc3pLMzVjTndpajN6MU09;MTUzMzg3QDMxMzcyZTMzMmUzMExzdXdhYzRFdG9vemJrWmdwQ0FlTWVKbHlNYjIrbmZycEFsS3M4NCtSYUk9;MTUzMzg4QDMxMzcyZTMzMmUzMEg1Vno4c25PM0RGQWs5bHVrK1llMzRNN1hWTHhVSGJJeHB1K2ZId1UzYlE9;MTUzMzg5QDMxMzcyZTMzMmUzMGRBTk92NUM2NktrQ2NyTUVpVlJQZWlCWGlKYVh5Wm4xdnlLSmQvZ1FIOXc9;MTUzMzkwQDMxMzcyZTMzMmUzMGFzRFNPSkg5RzV1aWJVdytNbldkaTJYQ0pkWG1xUzVaK2JsbWU3TlBwcnM9;MTUzMzkxQDMxMzcyZTMzMmUzME8yQ0lFSDNXL0lmdjhmZFpPVzA1ZlozdkFZNjBtdmxHa0gwSEhsdlRkOEE9;MTUzMzkyQDMxMzcyZTMzMmUzME9ySHRYeFE4L0lmQ1dvVlJqMUMxSHJ6SnY3RHNab0xRUDBlUjJ3MGJFbWc9;MTUzMzkzQDMxMzcyZTMzMmUzMGE0WWlYcGRWNWF2c1JmV1huS01DRys2UXc0KzhmWTY5K0tHRnV6OXlrajQ9;MTUzMzk0QDMxMzcyZTMzMmUzMEc4Q1Q0YTVRNDNFaHdZOTdQMFdHREQvVnd5Tlp0Y2EwYTA5clFFa0djM0U9;MTUzMzk1QDMxMzcyZTMzMmUzMGFzRFNPSkg5RzV1aWJVdytNbldkaTJYQ0pkWG1xUzVaK2JsbWU3TlBwcnM9;NT8mJyc2IWhiZH1gfWN9YmdoYmF8YGJ8ampqanNiYmlmamlmanMDHmg3Mj8/NjEmPz8TOzwnPjI6P30wPD4=");

      
            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new FriskaClient.MainPage());
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
