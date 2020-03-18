using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Forms;
using FriskaClient.Model;
using System.Collections.ObjectModel;
using FriskaClient.Services;
using System.Web;

namespace FriskaClient
{
    public partial class AllUsers : ContentPage
    {
        private ObservableCollection<User> AllaUsers { get; set; } 

        static readonly string  _url = App.url + "api/kontrollsvarsapi/";
        public AllUsers()
        {
            InitializeComponent();

            var vm = new UsersViewModel();
            usrList.ItemsSource = vm.AllUsers;
            AllaUsers = vm.AllUsers;


            usrList.ItemSelected += DeselectItem;


            ToolbarItem item = new ToolbarItem
            {
                Text = "Admin",


            };

            this.ToolbarItems.Add(item);
            item.Clicked += OnAdminClicked;
        }
        public async void OnAdminClicked(object sender, EventArgs args)
        {
            //await DisplayAlert("Todo", "Nu skulle du kommit till Admin Sidan", "Ok");
            await Navigation.PushAsync(new AdminPage());
        }
        public void DeselectItem(object sender, EventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }


        async void OnUserDetails(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserDetails());
        }

        async void OnThisUserDetails(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ThisUserDetails());         
        }
    }
}

