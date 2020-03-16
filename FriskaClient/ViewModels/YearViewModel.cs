﻿using System;
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
    class YearViewModel 
    {


        public static ObservableCollection<Year> _allYears = new ObservableCollection<Year>();
      
        public ObservableCollection<Year> AllYears { get { return _allYears; } }
       
        static public string yurl = App.url + "api/YearsApi/";
      
   
        public YearViewModel()
        {

            FillYearsAsync(App.Auth);
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