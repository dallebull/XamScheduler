
using Newtonsoft.Json;
using Syncfusion.SfCalendar.XForms;
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
    public class ViewModel
    {
        static string url = "https://31.208.194.94/api/kontrollsvarsapi/";
      
        private ObservableCollection<KontrollSvar> myAnswers = new ObservableCollection<KontrollSvar>();

         public ListView ansList = new ListView();
        public ObservableCollection<KontrollSvar> MyAnswers { get { return myAnswers; } }
        public ViewModel()
        {

            FillAnsAsync(App.Auth);
        }

        async void FillAnsAsync(string Auth)
        {         
            List<KontrollSvar> Answers = new List<KontrollSvar>();

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
            HttpClient client = new HttpClient(clientHandler);
    
           
                client.Timeout = TimeSpan.FromMinutes(10);
          
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Auth);
                var apiResponse = await client.GetStringAsync(url); 
                Answers = JsonConvert.DeserializeObject<List<KontrollSvar>>(apiResponse);

                foreach (var item in Answers)
                {
                    KontrollSvar ans = new KontrollSvar();
                    ans.ID = item.ID;
                    ans.UserID = item.UserID;
                    ans.Kontroll = item.Kontroll;
                    ans.KontrollTag = item.KontrollTag;
                    ans.RegDate = item.RegDate;
                    ans.Rattsvar = item.Rattsvar;
                    ans.YearID = item.YearID;



                    myAnswers.Add(ans);

                }
            
        }

    }
}


