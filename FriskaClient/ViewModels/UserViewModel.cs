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
        static public string aurl = App.url + "api/Account/IsAdmin";
        private string _id;
        public string Id
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
        private bool _isAdmin;
        public bool IsAdmin
        {
            get
            {
                return _isAdmin;
            }
            set
            {
                if (_isAdmin != value)
                {
                    _isAdmin = value;
                    OnPropertyChanged();
                }
            }
        }   
        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _gender;
        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                if (_gender != value)
                {
                    _gender = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _correctAnswers;
        public string CorrectAnswers
        {
            get
            {
                return _correctAnswers;
            }
            set
            {
                if (_correctAnswers != value)
                {
                    _correctAnswers = value;
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
        private string _namn;
        public string Namn
        {
            get
            {
                return _namn;
            }
            set
            {
                if (_namn != value)
                {
                    _namn = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateTime _joinDate;
        public DateTime JoinDate
        {
            get
            {
                return _joinDate;
            }
            set
            {
                if (_joinDate != value)
                {
                    _joinDate = value;
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
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };

                // Pass the handler to httpclient(from you are calling api)
                HttpClient client = new HttpClient(clientHandler);


                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Auth);
                var apiResponse = await client.GetStringAsync(url);


                var user = JsonConvert.DeserializeObject<List<JsonUser.RootObject>>(apiResponse);

                foreach (var item in user)
                {
                    Id = item.Id;
                    Age = item.Age;
                    CorrectAnswers = item.CorrectAnswers.ToString();
                    Email = item.Email;
                    Gender = item.Gender;
                    JoinDate = item.JoinDate;
                    PhoneNumber = item.PhoneNumber;
                    Namn = item.Namn;

                }
                aurl = aurl + "?Id=" + Id;
                StringContent scontent = new StringContent("?Id=" +Id, Encoding.UTF8, "application/json");
                var apiAnswer = await client.PostAsync(aurl, scontent);
                if (apiAnswer.IsSuccessStatusCode)
                {
                    IsAdmin = true;
                }
                else
                {
                    IsAdmin = false;
                }
            }
            catch (Exception)
            {


            }

        }

    }
}
