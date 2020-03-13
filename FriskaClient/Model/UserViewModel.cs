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

namespace FriskaClient.Model
{
    class UserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        static public string url = App.url + "api/Account/GetUser";
        private string _Email;
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                if (_Email != value)
                {
                    _Email = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _Gender;
        public string Gender
        {
            get
            {
                return _Gender;
            }
            set
            {
                if (_Gender != value)
                {
                    _Gender = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _CorrectAnswers;
        public string CorrectAnswers
        {
            get
            {
                return _CorrectAnswers;
            }
            set
            {
                if (_CorrectAnswers != value)
                {
                    _CorrectAnswers = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _Age;
        public int Age
        {
            get
            {
                return _Age;
            }
            set
            {
                if (_Age != value)
                {
                   
                    var Now = DateTime.Now.Year;
                    _Age = Now - value;
                    OnPropertyChanged();
                }
            }
        }
        private string _PhoneNumber;
        public string PhoneNumber
        {
            get
            {
                return _PhoneNumber;
            }
            set
            {
                if (_PhoneNumber != value)
                {
                    var builder = new StringBuilder(value);
                    builder.Insert(3, '-');     
                 
                    _PhoneNumber = builder.ToString();
                    OnPropertyChanged();
                }
            }
        }
        private string _Namn;
        public string Namn
        {
            get
            {
                return _Namn;
            }
            set
            {
                if (_Namn != value)
                {
                    _Namn = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateTime _JoinDate;
        public DateTime JoinDate
        {
            get
            {
                return _JoinDate;
            }
            set
            {
                if (_JoinDate != value)
                {
                    _JoinDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public UserViewModel()
        {

            LoadUser(App.Auth);
        }

        public async void LoadUser(string Auth)
        {

            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                // Pass the handler to httpclient(from you are calling api)
                HttpClient client = new HttpClient(clientHandler);


                client.Timeout = TimeSpan.FromMinutes(10);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Auth);
                var apiResponse = await client.GetStringAsync(url);


                var user = JsonConvert.DeserializeObject<List<JsonUser.RootObject>>(apiResponse);

                foreach (var item in user)
                {
                    
                    Age = item.Age;
                    CorrectAnswers = item.CorrectAnswers.ToString();
                    Email = item.Email;
                    Gender = item.Gender;
                    JoinDate = item.JoinDate;
                    PhoneNumber = item.PhoneNumber;
                    Namn = item.Namn;
                   
                }
         
            }
            catch (Exception)
            {


            }

        }

    }
}
