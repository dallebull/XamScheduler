using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ZXing.Mobile;
using Android.Graphics;

namespace FriskaClient.Android
{
    [Activity(Label = "FriskaClient", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            MobileBarcodeScanner.Initialize(Application);
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
        
            base.OnCreate(bundle);



            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
            Window.SetStatusBarColor(Color.Argb(255, 40, 40, 100));
            Window.SetNavigationBarColor(Color.Argb(255,40,40,100));          
            
            //Window.SetStatusBarColor(Color.Argb(255, 28, 34, 1));
            //Window.SetNavigationBarColor(Color.Argb(255,28,36,1));
        }
      
    }
}

