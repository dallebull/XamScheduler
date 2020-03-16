using FriskaClient.Model;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FriskaClient
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserDetails : ContentPage
    {

        public User ThisUser { get; set; }

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
            FriskaClient.Services.Settings.LastUsedPassword = null;
            App.IsUserLoggedIn = false;
            App.Auth = string.Empty;
            App.User = string.Empty;
            App.Devmode = false;
            App.Current.MainPage = new NavigationPage(new LoginPage())
            {

                BarBackgroundColor = Color.FromHex("#2828ff"),
                BarTextColor = Color.White
            };
        }
        public async void OnAdminClicked(object sender, EventArgs args)
        {
            //await DisplayAlert("Todo", "Nu skulle du kommit till Admin Sidan", "Ok");
            await Navigation.PushAsync(new AdminPage());
        }
    }
}