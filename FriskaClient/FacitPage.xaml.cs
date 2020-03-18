using FriskaClient.Model;
using FriskaClient.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Input;
using Xamarin.Forms;

namespace FriskaClient
{
    public partial class FacitPage : ContentPage
    {
        public string furl = App.url + "api/AdminApi/";
        private ObservableCollection<Facit> AllAnswers { get; set; }
        //private ObservableCollection<Year> AllYears { get; set; }
        public int YearId { get; set; }

        public FacitPage()
        {
          

            InitializeComponent();
            //var yr = new YearViewModel();
            var sv = new FacitViewModel();
            facList.ItemsSource = sv.AllFacits;
            AllAnswers = sv.AllFacits;

            facList.ItemSelected += DeselectItem;
            ToolbarItem item = new ToolbarItem
            {
                Text = App.User,


            };

            this.ToolbarItems.Add(item);
            item.Clicked += OnUserDetails;
        }
           public FacitPage(int Id)
        {
            InitializeComponent();

            YearId = Id;
            //var yr = new YearViewModel();
            var sv = new FacitViewModel(Id);
            facList.ItemsSource = sv.AllFacits;
            AllAnswers = sv.AllFacits;
            //AllYears = yr.AllYears;
            facList.ItemSelected += DeselectItem;
            ToolbarItem item = new ToolbarItem
            {
                Text = App.User,


            };

            this.ToolbarItems.Add(item);
            item.Clicked += OnUserDetails;
        }

        public void DeselectItem(object sender, EventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
        public async void OnAddClicked(object sender, EventArgs args)
        {

            int NextKontroll = AllAnswers.Count() + 1;
            await Navigation.PushAsync(new AddFacit(YearId, NextKontroll));
        }

        public async void OnYearClicked(object sender, EventArgs args)
        {
      
            await Navigation.PushAsync(new YearPage());
        }  
        public async void OnEditClicked(object sender, EventArgs args)
        {
            var item = (Xamarin.Forms.ImageButton)sender;
            var tmpId = item.CommandParameter;
            int Id = (int)tmpId;
            var tmpfacit = from f in AllAnswers where f.ID == Id select f;
            Facit facit = tmpfacit.FirstOrDefault();

            await Navigation.PushAsync(new EditFacitPage(facit));
  

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

                var response = await client.DeleteAsync(furl + "DeleteFacit/" + Id);
                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("", "Kontroll Borttagen", "Ok");

                    Navigation.InsertPageBefore(new FacitPage(YearId), this);
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
        async void OnUserDetails(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserDetails());
        }

    }
}