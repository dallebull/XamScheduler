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

namespace FriskaClient
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddFacit : ContentPage
    {

        static public string url = App.url + "api/AdminApi/";
        public AddFacit()
        {         
       
            InitializeComponent();
     
        }

        async void OnButtonClicked(object sender, EventArgs args)
        {
            Facit ks = new Facit();

            ks.KontrollTag = tagEntry.Text.ToUpper();
            ks.Kontroll = Int32.Parse(kontrollEntry.Text);

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sslsender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
            HttpClient client = new HttpClient(clientHandler);                  
     
            //Put Answer on Site
            var content = JsonConvert.SerializeObject(ks);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.Auth);
            StringContent scontent = new StringContent(content.ToString(), Encoding.UTF8, "application/json");
                var apiAnswer = await client.PostAsync(url, scontent);
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
     
    
