using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamScheduler.Model;

namespace XamScheduler
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookEvent : ContentPage
    {
        public DateTime Date { get; set; }
        public string Auth { get; set; }
        public BookEvent()
        {
            InitializeComponent();
          
        }
        public BookEvent(DateTime date, string Auth)
        {
         
            this.Date = DateTime.Parse(date.ToString("yyyy-MM-dd"));
            this.Auth = Auth;
            InitializeComponent();
            DatePickerLabel.Date =DateTime.Parse(date.ToString("yyyy-MM-dd"));
        }

        void OnTimePickerPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var t1 = startTimePicker.Time;
            t1 += TimeSpan.FromMinutes(30);
            var t2 = t1.ToString(@"hh\:mm");
            MinLabel.Text = "To: " + t2;
        }
        async void OnButtonClicked(object sender, EventArgs args)
        {
            var startDate = this.Date.ToString("yyyy/MM/dd");
            var StartTime = startDate + " " + startTimePicker.Time;
            if (Bookable(DateTime.Parse(StartTime)))
            {
      
            NewAppointment appointment = new NewAppointment();
            appointment.startDateTime = DateTime.Parse(StartTime);
            appointment.endDateTime = DateTime.Parse(StartTime).AddMinutes(30);
      

            var content = JsonConvert.SerializeObject(appointment);
         
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", Auth);
                StringContent scontent = new StringContent(content.ToString(), Encoding.UTF8, "application/json");
                var apiAnswer = await client.PostAsync("https://timebooking.azurewebsites.net/api/timebooking", scontent);
                    if (apiAnswer.IsSuccessStatusCode)
                    {
                     
                        Navigation.InsertPageBefore(new MainPage(Auth), this);
                        await Navigation.PopAsync();
                     
                    }
                    else
                    {
                        DisplayAlert("Sorry!", "Time Could not be Booked", "Ok");
                    }
                }
            }
            else
            {
                DisplayAlert("Sorry!", "Time Must be between 08:00-17:00!!", "Ok");
            }
        }
        bool Bookable(DateTime datetime)
        {
            TimeSpan start = new TimeSpan(08, 0, 0);
            TimeSpan end = new TimeSpan(17, 0, 0);
            TimeSpan now = datetime.TimeOfDay;
          

            if (start < end)
                return start <= now && now <= end;
            // start is after end, so do the inverse comparison
            return !(end < now && now < start);
        }
    }
}