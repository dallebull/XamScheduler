using System.Collections.Generic;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using FriskaClient.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;

namespace FriskaClient.Model
{
    class YearViewModel: INotifyPropertyChanged
    {


        private static ObservableCollection<Year> _allYears = new ObservableCollection<Year>();

        private static DateTime LastUpdate  { get; set; }
 
    public ObservableCollection<Year> AllYears
        {
            get { return _allYears; }
            set
            {
                if (value != _allYears)
                {
                    this.AllYears = value;
                    NotifyPropertyChanged();
                } 
            } 
        }
       
        static public string yurl = App.url + "api/YearsApi/";
      
   
        public YearViewModel()
        {

            FillYearsAsync(App.Auth);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       

        async void FillYearsAsync(string Auth)
        {

            if (LastUpdate.CompareTo(DateTime.Now.AddSeconds(10)) > 0)
            {
                LastUpdate = DateTime.Now;
                _allYears.Clear();
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

}