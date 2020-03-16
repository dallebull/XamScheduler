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

        static readonly string  _url = App.url + "api/YearApi/";
        public YearPage()
        {
            InitializeComponent();

            var vm = new YearViewModel();
            yearList.ItemsSource = vm.AllYears;
            MyYears = vm.AllYears;
            yearList.ItemSelected += DeselectItem;

        }
        public void DeselectItem(object sender, EventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
        async void OnAddButtonClicked(object sender, EventArgs args)
        {
           // await Navigation.PushAsync(new AddYear());
        }    
   
        async void OnDelClicked(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.ImageButton)sender;
            var Id = item.CommandParameter;
            var IdString = Id.ToString();
            var tmpkontroll = from a in MyYears where a.ID.ToString() == IdString select a;
            var kontroll = tmpkontroll.First() as Year;

            var action = await DisplayAlert("Ta Bort?", "Vill du ta bort Året?\n" +kontroll.YearName, "Ja", "Nej");
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
             
                    Navigation.InsertPageBefore(new MainPage(), this);
                    await Navigation.PopAsync();
                }
                else
                {
                    try
                    {
                        var ex = ApiException.CreateApiException(response);
                        if (ex.Errors.Count() == 1)
                        {
                            await DisplayAlert("Oh No!", ex.Errors.FirstOrDefault().ToString(), "Ok");
                        }
                        else
                        {
                            for (int i = 0; i < ex.Errors.Count(); i++)
                            {
                                await DisplayAlert("Oh No!", ex.Errors.ElementAt(i).ToString(), "Ok");
                            }

                        }

                    }
                    catch (Exception)
                    {

                        await DisplayAlert("Oh No!", "Något allvarligt gick fel!", "Ok");
                    }
                }     
        }

        }

    async void OnEditClicked(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.ImageButton)sender;
            var Id = (int)item.CommandParameter;
            await Navigation.PushAsync(new AdminPage(Id));

        }
    }
}
