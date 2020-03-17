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

        static string url = App.url + "api/KontrollSvarsApi";
        public AddKontroll()
        {         
       
            InitializeComponent();
            kontrollEntry.Completed += (sender, args) => { tagEntry.Focus(); };
            tagEntry.Completed += (sender, args) => { OnButtonClicked(null, null); };
            ToolbarItem item = new ToolbarItem
            {
                Text = App.User,


            };

            this.ToolbarItems.Add(item);
            item.Clicked += OnUserDetails;
        }

        async void OnButtonClicked(object sender, EventArgs args)
        {
            KontrollSvar ks = new KontrollSvar();
            try
            {
                ks.Kontroll = Int32.Parse(kontrollEntry.Text);
                ks.KontrollTag = tagEntry.Text.ToUpper();

            }
            catch (Exception)
            {
            
            }

            if (ks.Kontroll != 0 && ks.KontrollTag != null)
            {


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
            else
            {
                await DisplayAlert("Fel!", "Fyll i alla Fält!", "Ok");
            }
        }
        async void OnUserDetails(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserDetails());
        }

    }

}
     
    
