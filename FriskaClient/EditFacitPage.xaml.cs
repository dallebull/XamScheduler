using FriskaClient.Model;
using FriskaClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FriskaClient
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditFacitPage : ContentPage
    {
        static public string furl = App.url + "api/AdminApi/";
        public EditFacitPage()
        {
            InitializeComponent();
  

        }   
        public EditFacitPage(int Id)
        {
            var fm = new FacitViewModel();

            var tmpFacit = fm.AllFacits;
            var facit = from f in tmpFacit where f.ID == Id select f;
            var thisfacit = facit.FirstOrDefault() as Facit;

            InitializeComponent();
            this.BindingContext = thisfacit;

        }
        async void OnEditClicked(object sender, EventArgs args)
        {
            Facit fc = new Facit();

            try
            {
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
            var apiAnswer = await client.PutAsync(furl, scontent);
            if (apiAnswer.IsSuccessStatusCode)
            {
                this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
                Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync();
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
    }
}