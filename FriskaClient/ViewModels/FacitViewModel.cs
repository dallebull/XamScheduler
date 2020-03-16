using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using FriskaClient.Model;
using FriskaClient.Models;

namespace FriskaClient.Model
{
    class FacitViewModel 
    {
        static public string furl = App.url + "api/AdminApi/";

        private ObservableCollection<Facit> _allFacits = new ObservableCollection<Facit>();
  
        public ObservableCollection<Facit> AllFacits { get { return _allFacits; } }

      
        public FacitViewModel()
        {

            FillFacAsync(App.Auth);
        }
        async void FillFacAsync(string Auth)
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

    }
  

}