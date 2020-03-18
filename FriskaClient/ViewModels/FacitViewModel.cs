using System.Collections.Generic;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using FriskaClient.Models;
using System.Threading.Tasks;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FriskaClient.Model
{
    class FacitViewModel
    {
        static public string furl = App.url + "api/AdminApi/GetFacits";
        static public string aurl = App.url + "api/AdminApi/GetFacit";

        private ObservableCollection<Facit> _allFacits = new ObservableCollection<Facit>();
  
        public ObservableCollection<Facit> AllFacits { get { return _allFacits; } }

      
        public  FacitViewModel()
        {
            FillFacAsync(App.Auth);

        }
        public  FacitViewModel(int Id)
        {
           
          FillFacAsync(App.Auth, Id);

        }



            private async void FillFacAsync(string Auth)
            {
            List<Facit> Answers = new List<Facit>();

            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            // Pass the handler to httpclient(from you are calling api)
            HttpClient client = new HttpClient(clientHandler);



            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Auth);
            var apiResponse = await client.GetStringAsync(furl);
            Answers = JsonConvert.DeserializeObject<List<Facit>>(apiResponse);

            foreach (var item in Answers)
            {
                Facit fac = new Facit
                {
                    ID = item.ID,
                    Kontroll = item.Kontroll,
                    KontrollTag = item.KontrollTag,
                    QRURL = item.QRURL,
                    YearID = item.YearID,
                };
                _allFacits.Add(fac);

            }
            
        }
        private async void FillFacAsync(string Auth, int Id)
        {
            List<Facit> Answers = new List<Facit>();

            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            // Pass the handler to httpclient(from you are calling api)
            HttpClient client = new HttpClient(clientHandler);



            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Auth);
            var apiResponse = await client.GetStringAsync(furl);
            Answers = JsonConvert.DeserializeObject<List<Facit>>(apiResponse);

            foreach (var item in Answers)
            {
                if (item.YearID == Id)
                {

            
                Facit fac = new Facit
                {
                    ID = item.ID,
                    Kontroll = item.Kontroll,
                    KontrollTag = item.KontrollTag,
                    QRURL = item.QRURL,
                    YearID = item.YearID,
                };
                _allFacits.Add(fac);
                }
            }

        }

    }
  

}