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
    public partial class AddKontroll : ContentPage
    {

        static string url = "https://31.208.194.94/api/KontrollSvarsApi";
        public AddKontroll()
        {         
       
            InitializeComponent();
     
        }

        async void OnButtonClicked(object sender, EventArgs args)
        {
            KontrollSvar ks = new KontrollSvar();

            ks.KontrollTag = tagEntry.Text;
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
                await DisplayAlert("Oh No!", apiAnswer.StatusCode.ToString(), "Ok") ;
                    }
        }
    }
  
 }
     
    
