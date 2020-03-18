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
 
    public partial class EditFacitPage : ContentPage
    {
        static public string furl = App.url + "api/AdminApi/";
        public EditFacitPage()
        {
            InitializeComponent();
  

        }   
        public EditFacitPage(Facit facit)
        {
            InitializeComponent();
            this.BindingContext = facit;
            kontrollEntry.Completed += (sender, args) => { tagEntry.Focus(); };
            tagEntry.Completed += (sender, args) => { OnEditClicked(sender,  args); };

            ToolbarItem item = new ToolbarItem
            {
                Text = "Admin",


            };

            this.ToolbarItems.Add(item);
            item.Clicked += OnAdminClicked;
        }
        public async void OnAdminClicked(object sender, EventArgs args)
        {
            //await DisplayAlert("Todo", "Nu skulle du kommit till Admin Sidan", "Ok");
            await Navigation.PushAsync(new AdminPage());
        }

        async void OnEditClicked(object sender, EventArgs args)
        {
  
            var Id = idEntry.Text;
            var YearId = YearidEntry.Text;

            Facit fc = new Facit();

            try
            {
                fc.YearID = Int32.Parse(YearId);
                fc.ID = Int32.Parse(Id);
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

            var thisUrl = furl + "PutFacit/" +fc.ID;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.Auth);
            StringContent scontent = new StringContent(content.ToString(), Encoding.UTF8, "application/json");
            var apiAnswer = await client.PutAsync(thisUrl, scontent);
            if (apiAnswer.IsSuccessStatusCode)
            {
                await DisplayAlert("", "Kontroll Ändrad!", "Ok");
                this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
                Navigation.InsertPageBefore(new FacitPage(fc.YearID), this);
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
        async void OnUserDetails(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserDetails());
        }

    }
}