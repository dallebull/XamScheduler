using Newtonsoft.Json;
using NUnit.Framework;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamScheduler.Model;

namespace XamScheduler
{
    public partial class MainPage : ContentPage
    {
        public string Auth { get; set; }
        public DateTime Date { get; set; }

        public MainPage()
        {
            App.Auth = Auth;

                InitializeComponent();

        }
        public MainPage(string Auth)
        {
            Navigation.PopAsync();
            this.Auth = Auth;
            InitializeComponent();       
            
        }

        public async void OnDateCellHolding(object sender, Syncfusion.SfCalendar.XForms.DayCellHoldingEventArgs e)
        {
        //    var  HasBooking = false;
        //    var ThisDate = e.Calendar.SelectedDate.Value.Date;

        //    if ((e.Calendar.DataSource as CalendarEventCollection).Count != 0 )
        //    {
         
        //        foreach (var item in e.Calendar.DataSource as CalendarEventCollection)
        //        {

        //            if (item.Subject != "" && item.StartTime.Date == ThisDate)
        //            {
        //                HasBooking = true;

        //                bool answer = await DisplayAlert(item.StartTime.ToString("yyyy-MM-dd"), "Would you like to Remove this Booking", "Yes", "No");
        //                if (answer)
        //                {
        //                    int id = (item as CustomAppointment).EventId;
                       
        //                    RemoveEvent(id, Auth);
        //                }
        //            }
              
        //        }
        //    }
        //if(!HasBooking)
        //    {

                BookEventQuery(calendar.SelectedDate);

            //}
            //Don't need this since u made the lower method.

        }
        private async void Calendar_InlineItemTapped(object sender, InlineItemTappedEventArgs e)
        {
            var appointment = e.InlineEvent as CustomAppointment;
            var id = appointment.EventId;
            if (appointment.Subject != "")
            {
                bool app = await DisplayAlert(appointment.Subject, appointment.StartTime.ToString("yyyy-MM-dd HH:mm"), "Ok", "Remove");
                if (!app)
                {
                    RemoveEvent(id, Auth);
                }
            }
            else
            {
                DisplayAlert("Allready Taken", appointment.StartTime.ToString("yyyy-MM-dd HH:mm"), "Ok");
            }
      
        }

        async void Handle_InlineToggled(object sender, InlineToggledEventArgs e)
        {
           
        }

        async void BookEventQuery(object sender)
        {
            

            var tmpDate = calendar.SelectedDate.ToString();
            var tmpDate2 = DateTime.Parse(tmpDate);
            var DateDate = tmpDate2.Date.ToString("yyyy-MM-dd");
            bool answer = await DisplayAlert(DateDate, "Would you like to Book this Day", "Yes", "No");
            if (answer == true)
            {
                this.Date = (DateTime.Parse(calendar.SelectedDate.ToString()));
                Navigation.PushAsync( new BookEvent((DateTime.Parse(calendar.SelectedDate.ToString())), Auth));
                Navigation.RemovePage(this);

            }
        }

        public async void RemoveEvent(int id, string Auth)
        {
            bool answer = await DisplayAlert("Delete", "Are you 110% Sure?", "Yes", "No");
            if (answer)
            {
                try
                {

                
                    using (HttpClient client = new HttpClient())
                    {
                        var url = "https://timebooking.azurewebsites.net/api/timebooking/" + id;

                        var DelAuth = new Dictionary<string, string>();
                        DelAuth.Add("Authorization", "Bearer " + Auth);
                        DelAuth.Add("Content-Type", "application/x-www-form-urlencoded");
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Auth);
                        var req = new HttpRequestMessage(HttpMethod.Delete, url) { Content = new FormUrlEncodedContent(DelAuth) };
                        var res = await client.SendAsync(req);
               
                        if (res.IsSuccessStatusCode)
                        {
                            Navigation.InsertPageBefore(new MainPage(Auth), this);
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            DisplayAlert("Error!", "Could not Delete Booking!!", "Ok");
                        }
                    }
                }
                catch (Exception)
                {
                    DisplayAlert("Error!", "My Name is Error!!", "Hello");
                }
                }
            }
        }
 }

