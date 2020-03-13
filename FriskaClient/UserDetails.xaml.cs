using FriskaClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


using Newtonsoft.Json;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Forms;
using FriskaClient.Model;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;

namespace FriskaClient
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserDetails : ContentPage
    {

        public User thisUser { get; set; }

        public UserDetails()
        {
            InitializeComponent();

            this.BindingContext = new UserViewModel();

            ToolbarItem item = new ToolbarItem
            {
                Text = "Logga ut"
            };

            this.ToolbarItems.Add(item);
            item.Clicked += OnLogout;
        
        }
   

        static void OnLogout(object sender, EventArgs e)
        {

            App.IsUserLoggedIn = false;
            App.Auth = string.Empty;
            App.User = string.Empty;
            App.Devmode = false;
            App.Current.MainPage = new NavigationPage(new LoginPage())
            {

                BarBackgroundColor = Color.FromHex("#ed1c24"),
                BarTextColor = Color.White
            };
        }
    }
}