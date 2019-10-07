using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamScheduler.Model;

namespace XamScheduler
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }



        public async void OnDateCellHolding(object sender, EventArgs e)
        {
            var ClickedTime = (DateTime)calendar.SelectedDate;
            NewAppointment appointment = new NewAppointment();
            appointment.startDateTime = ClickedTime;
            appointment.endDateTime = ClickedTime.AddMinutes(30);
            appointment.name = "Björne Testar";
         
            HttpClient client = new HttpClient();

            var content = JsonConvert.SerializeObject(appointment);
            await client.PostAsync("https://timebookingapi.azurewebsites.net/api/timebooking", new StringContent(content));

        }
    }
}
