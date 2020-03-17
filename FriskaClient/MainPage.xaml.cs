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
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<KontrollSvar> MyAnswers { get; set; } 

        static readonly string  _url = App.url + "api/kontrollsvarsapi/";
        public MainPage()
        {
            InitializeComponent();

            var vm = new SvarViewModel();
            ansList.ItemsSource = vm.MyAnswers;
            MyAnswers = vm.MyAnswers;


            ansList.ItemSelected += DeselectItem;


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
        async void OnAddButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new AddKontroll());
        }    
        async void OnScanButtonClicked(object sender, EventArgs args)
        {

            string Kontroll = null;
            string KontrollTag = null;
            try
            {
                var scanner = DependencyService.Get<IQrScanningService>();
             
                var result = await scanner.ScanAsync();
                if (result != null)
                {
                    Uri myUri = new Uri(result);
                    Kontroll = HttpUtility.ParseQueryString(myUri.Query).Get("Kontroll");
                    KontrollTag = HttpUtility.ParseQueryString(myUri.Query).Get("KontrollTag");
                }
            }
            catch (Exception)
            {

                throw;
            }

            if (Kontroll != null && KontrollTag != null)
            {


                KontrollSvar ks = new KontrollSvar
                {
                    KontrollTag = KontrollTag,
                    Kontroll = Int32.Parse(Kontroll)
                };

                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sslsender, cert, chain, sslPolicyErrors) => { return true; }
                };

                // Pass the handler to httpclient(from you are calling api)
                HttpClient client = new HttpClient(clientHandler);

                //Put Answer on Site
                var content = JsonConvert.SerializeObject(ks);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.Auth);
                StringContent scontent = new StringContent(content.ToString(), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(_url, scontent);
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

        async void OnDelClicked(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.ImageButton)sender;
            var Id = item.CommandParameter;
            var IdString = Id.ToString();
            var tmpkontroll = from a in MyAnswers where a.ID.ToString() == IdString select a;
            var kontroll = tmpkontroll.First() as KontrollSvar;

            var action = await DisplayAlert("Ta Bort?", "Vill du ta bort Kontrollen?\n" +kontroll.Kontroll + "  " + kontroll.KontrollTag, "Ja", "Nej");
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

