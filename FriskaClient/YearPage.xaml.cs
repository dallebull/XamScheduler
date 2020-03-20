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
using FriskaClient.Models;

namespace FriskaClient
{
    public partial class YearPage : ContentPage
    {
        private ObservableCollection<Year> MyYears { get; set; } 

         readonly string  _url = App.url + "api/YearsApi/DeleteYear/";
        public YearPage()
        {
            InitializeComponent();
       
            var fc = new FacitViewModel();
            var vm = new YearViewModel();
            yearList.ItemsSource = YearViewModel.AllYears;
            MyYears = YearViewModel.AllYears; 
            yearList.ItemSelected += OnEditClicked;
            ToolbarItem item = new ToolbarItem
            {
                Text = "Admin",


            };

            this.ToolbarItems.Add(item);
            item.Clicked += OnAdminClicked;
        }
        public async void OnAdminClicked(object sender, EventArgs args)
        {
         
            await Navigation.PushAsync(new AdminPage());
      
        }

        async void OnAddButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new AddYear());
        }    
   
        async void OnDelClicked(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.ImageButton)sender;
            var Id = item.CommandParameter;
            var IdString = Id.ToString();
            var tmpkontroll = from a in MyYears where a.ID.ToString() == IdString select a;
            var kontroll = tmpkontroll.First() as Year;

            var action = await DisplayAlert("Ta Bort?", "Vill du ta bort År: " +kontroll.YearName +"?!\nÄven Kontroller och Svar kommer försvinna!", "Ja", "Nej");
            if (action)
            {


                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (thissender, cert, chain, sslPolicyErrors) => { return true; }
                };

                // Pass the handler to httpclient(from you are calling api)
                HttpClient client = new HttpClient(clientHandler)
                {
                    Timeout = TimeSpan.FromMinutes(10)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.Auth);
          
                var response = await client.DeleteAsync(_url + Id);
                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("", "År Borttaget", "Ok");
            
                    Navigation.InsertPageBefore(new YearPage(), this);
                    await Navigation.PopAsync();
                }
                else
                {
                    try
                    {
                        var ex = ApiException.CreateApiException(response);
                        if (ex.Errors.Count() == 1)
                        {
                            await DisplayAlert("Fel!", ex.Errors.FirstOrDefault().ToString(), "Ok");
                        }
                        else
                        {
                            for (int i = 0; i < ex.Errors.Count(); i++)
                            {
                                await DisplayAlert("Fel!", ex.Errors.ElementAt(i).ToString(), "Ok");
                            }

                        }

                    }
                    catch (Exception)
                    {

                        await DisplayAlert("Fel!", "Något allvarligt gick fel!", "Ok");
                    }
                }     
        }

        }



        public void OnEditClicked(object sender, EventArgs e)
        {
            if (((ListView)sender).SelectedItem != null)
            {
                var selectedItem = ((ListView)sender).SelectedItem as Year;
                int Id = selectedItem.ID;
                ((ListView)sender).SelectedItem = null;

                Navigation.PushAsync(new FacitPage(Id));

            }
        }

        async void OnAllUsersButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AllUsers());
        }   


    }
}

