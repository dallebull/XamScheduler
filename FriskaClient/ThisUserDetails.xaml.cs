using FriskaClient.Model;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FriskaClient
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThisUserDetails : ContentPage
    {

        public User ThisUser { get; set; }

        public ThisUserDetails()
        {
            InitializeComponent();

            this.BindingContext = new UserViewModel();
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
            await Navigation.PushAsync(new YearPage());
        }
    }
}