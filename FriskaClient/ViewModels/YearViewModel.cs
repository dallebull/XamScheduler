using System.Collections.Generic;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using FriskaClient.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FriskaClient.Model
{
    class YearViewModel
    {


        private static ObservableCollection<Year> _allYears = new ObservableCollection<Year>();

        private static DateTime LastUpdate { get; set; } = DateTime.Now;
        public string ThisYear { get; set; }
        public static ObservableCollection<Year> AllYears
        {
            get { return _allYears; }
            set
            {
                if (value != _allYears)
                {
                    AllYears = value;
                   
                } 
            } 
        }
       
        static public string yurl = App.url + "api/YearsApi/";
      
   
        public YearViewModel()
        {
            DateTime nextUpdate = LastUpdate.AddSeconds(1);
            var Update = DateTime.Compare(nextUpdate, DateTime.Now);
            if (Update < 0)
            {
                LastUpdate = DateTime.Now;
                _allYears.Clear();

                FillYearsAsync(App.Auth);
            }

        }
         
      public async void GetYearName(int Id)
        {
            var Auth = App.Auth;
            List<Year> Answers = new List<Year>();

            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            // Pass the handler to httpclient(from you are calling api)
            HttpClient client = new HttpClient(clientHandler);



            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Auth);
            var apiResponse = await client.GetStringAsync(yurl);
            Answers = JsonConvert.DeserializeObject<List<Year>>(apiResponse);

            foreach (var item in Answers)
            {
                Year year = new Year
                {
                    ID = item.ID,         
                    YearName = item.YearName,
                    YearID = item.YearID,
                };
          
                    _allYears.Add(year);             

            }
            var tmpreturnstring = from y in _allYears where y.ID == Id select y.YearName;
            var areturnstring = tmpreturnstring.ToList();
             ThisYear = areturnstring.First();
      
        }

        async void FillYearsAsync(string Auth)
        {
        
            List<Year> Answers = new List<Year>();

            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            // Pass the handler to httpclient(from you are calling api)
            HttpClient client = new HttpClient(clientHandler);



            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Auth);
            var apiResponse = await client.GetStringAsync(yurl);
            Answers = JsonConvert.DeserializeObject<List<Year>>(apiResponse);

            foreach (var item in Answers)
            {
                Year year = new Year
                {
                    ID = item.ID,         
                    YearName = item.YearName,
                    YearID = item.YearID,
                };
          
                    _allYears.Add(year);
               

            }
         }
        

    } 

}