using FriskaClient.Model;
using FriskaClient.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Forms;

namespace FriskaClient
{
    public partial class AdminPage : ContentPage
    {
        static public string furl = App.url + "api/AdminApi/";
        private ObservableCollection<Facit> AllAnswers { get; set; }

        public AdminPage()
        {
            InitializeComponent();
            var yr = new YearViewModel();
            var sv = new FacitViewModel();
            facList.ItemsSource = sv.AllFacits;
            AllAnswers = sv.AllFacits;

            facList.ItemSelected += DeselectItem;

         
            //ToolbarItem item = new ToolbarItem
            //{
            //    Text = App.User,
            //};

            //this.ToolbarItems.Add(item);
            //item.Clicked += OnUserDetails;

        }

        public void DeselectItem(object sender, EventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
        public async void OnAddClicked(object sender, EventArgs args)
        {
            await DisplayAlert("Todo", "Nu skulle du kommit till Lägg Till Sidan", "Ok");
            //await Navigation.PushAsync(new AddFacit());
        }

        public async void OnYearClicked(object sender, EventArgs args)
        {
            //await DisplayAlert("Todo", "Nu skulle du kommit till Admin Sidan", "Ok");
            await Navigation.PushAsync(new YearPage());
        }  
        public async void OnEditClicked(object sender, EventArgs args)
        {
            await DisplayAlert("Todo", "Nu skulle du kommit Edit Sidan", "Ok");

        }
        async void OnDelClicked(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.ImageButton)sender;
            var Id = item.CommandParameter;
            var IdString = Id.ToString();
            var tmpkontroll = from a in AllAnswers where a.ID.ToString() == IdString select a;
            var kontroll = tmpkontroll.First() as Facit;

            var action = await DisplayAlert("Ta Bort?", "Vill du ta bort Facit?\n" + kontroll.Kontroll + "  " + kontroll.KontrollTag, "Ja", "Nej");
            if (action)
            {


                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (thissender, cert, chain, sslPolicyErrors) => { return true; }
                };

                // Pass the handler to httpclient(from you are calling api)
                HttpClient client = new HttpClient(clientHandler);        

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.Auth);

                var response = await client.DeleteAsync(furl + Id);
                if (response.IsSuccessStatusCode)
                {

                    Navigation.InsertPageBefore(new AdminPage(), this);
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
    }
}