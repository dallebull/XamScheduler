using Newtonsoft.Json;
using Syncfusion.SfCalendar.XForms;
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

            var content = JsonConvert.SerializeObject(appointment);

            using (HttpClient client = new HttpClient())
            {

                StringContent scontent = new StringContent(content.ToString(), Encoding.UTF8, "application/json");
                

                await client.PostAsync("https://timebookingapi.azurewebsites.net/api/timebooking", scontent);
       
                InitializeComponent();
            }
        }
      
    }
}
