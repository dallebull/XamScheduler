
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
        public ObservableCollection<KontrollSvar> myAnswers { get; set; } = new ObservableCollection<KontrollSvar>();

 
        public ObservableCollection<KontrollSvar> MyAnswers { get { return myAnswers; } }
        public ViewModel()
        {

            FillApsAsync(App.Auth);
        }

        async void FillApsAsync(string Auth)
        {

            var ansList = new ListView();
            ansList.ItemsSource = myAnswers;

            List<KontrollSvar> Answers = new List<KontrollSvar>();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Auth);
                var apiResponse = await client.GetStringAsync("http://31.208.194.94/api/kontrollsvarsapi/");
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
}


