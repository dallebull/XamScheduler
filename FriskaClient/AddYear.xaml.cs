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
    public partial class AddYear : ContentPage
    {

        static public string url = App.url + "api/YearsApi/";
        public AddYear()
        {         
       
            InitializeComponent();
     
            yearEntry.Completed += (sender, args) => { OnButtonClicked(null, null); };

        }

        async void OnButtonClicked(object sender, EventArgs args)
        {
            Year ks = new Year();

            try
            {
                ks.YearName = yearEntry.Text.ToUpper();
            
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
            var content = JsonConvert.SerializeObject(ks);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.Auth);
            StringContent scontent = new StringContent(content.ToString(), Encoding.UTF8, "application/json");
                var apiAnswer = await client.PostAsync(url, scontent);
                    if (apiAnswer.IsSuccessStatusCode)
                    {
                await DisplayAlert("", "År Tillagt", "Ok");
                this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
                        Navigation.InsertPageBefore(new YearPage(), this);
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
     
    
