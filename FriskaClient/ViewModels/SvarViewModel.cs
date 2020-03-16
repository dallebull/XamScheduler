
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Forms;
using FriskaClient.Model;
using System.Collections.ObjectModel;

namespace FriskaClient
{
    public class SvarViewModel
    {
        static readonly string _url = App.url + "api/kontrollsvarsapi/";      

        private ObservableCollection<KontrollSvar> _myAnswers = new ObservableCollection<KontrollSvar>();
    
        public ObservableCollection<KontrollSvar> MyAnswers { get { return _myAnswers; } }
        public SvarViewModel()
        {

            FillAnsAsync(App.Auth);
        }

        async void FillAnsAsync(string Auth)
        {         
            List<KontrollSvar> Answers = new List<KontrollSvar>();

            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            // Pass the handler to httpclient(from you are calling api)
            HttpClient client = new HttpClient(clientHandler);



            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Auth);
                var apiResponse = await client.GetStringAsync(_url); 
                Answers = JsonConvert.DeserializeObject<List<KontrollSvar>>(apiResponse);

                foreach (var item in Answers)
                {
                    KontrollSvar ans = new KontrollSvar
                    {
                        ID = item.ID,
                        UserID = item.UserID,
                        Kontroll = item.Kontroll,
                        KontrollTag = item.KontrollTag,
                        RegDate = item.RegDate,
                        Rattsvar = item.Rattsvar,
                        YearID = item.YearID
                    };
                _myAnswers.Add(ans);

                }
            
        }

    }
}


