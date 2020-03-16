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
    class FacitViewModel : INotifyPropertyChanged
    {


        private ObservableCollection<Facit> _allFacits = new ObservableCollection<Facit>();

        public ListView facList = new ListView();
        public ObservableCollection<Facit> AllFacits { get { return _allFacits; } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        static public string furl = App.url + "api/Facits/";

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
        private int _kontroll;
        public int Kontroll
        {
            get
            {
                return _kontroll;
            }
            set
            {
                if (_kontroll != value)
                {
                    _kontroll = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _kontrollTag;
        public string KontrollTag
        {
            get
            {
                return _kontrollTag;
            }
            set
            {
                if (_kontrollTag != value)
                {
                    _kontrollTag = value;
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
        private string _qrurl;
        public string QRURL
        {
            get
            {
                return _qrurl;
            }
            set
            {
                if (_qrurl != value)
                {
                    _qrurl = value;
                    OnPropertyChanged();
                }
            }
        }
   
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
                Facit fac = new Facit();
                {
                    ID = item.ID;
                    Kontroll = item.Kontroll;
                    KontrollTag = item.KontrollTag;
                    QRURL = item.QRURL;
                    YearID = item.YearID;
                };
                _allFacits.Add(fac);

            }

        }

    }
  

}