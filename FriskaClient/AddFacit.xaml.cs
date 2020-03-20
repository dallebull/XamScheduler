using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FriskaClient.Model;
using FriskaClient.Models;
using System.Collections.ObjectModel;
using Android.Content.Res;

namespace FriskaClient
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddFacit : ContentPage
    {

        static public string url = App.url + "api/AdminApi/";

        public int NextKontroll { get; set; }
        public int YearId { get; set; }
 
        public AddFacit( int YearId, int NextKontroll)
        {
         

            InitializeComponent();
            this.YearId = YearId;
            this.NextKontroll = NextKontroll;
            BindingContext = this;
            ToolbarItem item = new ToolbarItem
            {
                Text = App.User,


            };

            this.ToolbarItems.Add(item);
            item.Clicked += OnUserDetails;

            kontrollEntry.Completed += (sender, args) => { tagEntry.Focus(); };
            tagEntry.Completed += (sender, args) => { OnButtonClicked(null, null); };

        }    


        async void OnButtonClicked(object sender, EventArgs args)
        {
            Facit fc = new Facit();

            try
            {
                fc.YearID = YearId;
                fc.KontrollTag = tagEntry.Text.ToUpper();
                fc.Kontroll = Int32.Parse(kontrollEntry.Text);
            }
            catch (Exception)
            {
                await DisplayAlert("Fel!", "Fyll i alla Fält!", "Ok");
            }
         

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sslsender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
            HttpClient client = new HttpClient(clientHandler);                  
     
            //Put Answer on Site
            var content = JsonConvert.SerializeObject(fc);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.Auth);
            StringContent scontent = new StringContent(content.ToString(), Encoding.UTF8, "application/json");
                var apiAnswer = await client.PostAsync(url, scontent);
                    if (apiAnswer.IsSuccessStatusCode)
                    {
                        var anotherone =   await DisplayAlert("", "Kontroll Tillagd", "Lägg till en till" ,"Tillbaka");
                        if (anotherone)
                        {
                            await Navigation.PushAsync(new AddFacit(YearId, fc.Kontroll +1));
                        }

                        else
                        {
                            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);

                            Navigation.InsertPageBefore(new FacitPage(YearId), this);
                            await Navigation.PopAsync();
                        }                     
                    }
                    else
                    {
                try
                {
                    var ex = ApiException.CreateApiException(apiAnswer);
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
        async void OnUserDetails(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserDetails());
        }

    }
}
     
    
