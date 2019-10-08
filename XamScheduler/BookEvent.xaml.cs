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
            this.Date = date;
            this.Auth = Auth;
            InitializeComponent();
        }

        void OnTimePickerPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
       
        }
        async void OnButtonClicked(object sender, EventArgs args)
        {
            var startDate = this.Date.ToString("yyyy/MM/dd");
            var StartTime = startDate + " " + startTimePicker.Time;      
                       
            NewAppointment appointment = new NewAppointment();
            appointment.startDateTime = DateTime.Parse(StartTime);
            appointment.endDateTime = DateTime.Parse(StartTime).AddMinutes(30);
            appointment.name = "Björne Testar";

            var content = JsonConvert.SerializeObject(appointment);
         
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", Auth);
                StringContent scontent = new StringContent(content.ToString(), Encoding.UTF8, "application/json");
                await client.PostAsync("https://timebooking.azurewebsites.net/api/timebooking", scontent);

                Navigation.PushAsync( new XamScheduler.MainPage(Auth)); 
            }
        }
    }
}