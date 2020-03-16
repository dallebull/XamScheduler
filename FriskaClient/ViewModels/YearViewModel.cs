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
    class YearViewModel : INotifyPropertyChanged
    {


        private ObservableCollection<Year> _allYears = new ObservableCollection<Year>();

      
        public ObservableCollection<Year> AllYears { get { return _allYears; } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

       
        static public string yurl = App.url + "api/Years/";
        private int _id;
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }    
        private int _yearID;
        public int YearID
        {
            get
            {
                return _yearID;
            }
            set
            {
                if (_yearID != value)
                {
                    _yearID = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _yearname;
        public string YearName
        {
            get
            {
                return _yearname;
            }
            set
            {
                if (_yearname != value)
                {
                    _yearname = value;
                    OnPropertyChanged();
                }
            }
        }
   
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
                Year year = new Year();
                {
                    ID = item.ID;
         
                    YearName = item.YearName;
                    YearID = item.YearID;
                };
                _allYears.Add(year);

            }

        }

    } 

}