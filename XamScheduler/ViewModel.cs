
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
using XamScheduler.Model;

namespace XamScheduler
{
    public class ViewModel
    {
        public string Auth { get; set; }
        public CalendarEventCollection CalendarInlineEvents { get; set; } = new CalendarEventCollection();

        public ViewModel()
        {

        }
        public ViewModel(string Auth)
        {
            // Create events 
            this.Auth = Auth;
            FillApsAsync(Auth);
            #region Old
            //ApService ap = new ApService();
            //var ApList = ap.FillAps(Auth);

            //foreach (var item in ApList)
            //{
            //    CalendarInlineEvent event1 = new CalendarInlineEvent();
            //    event1.StartTime = item.startDateTime;
            //    event1.EndTime = item.endDateTime;
            //    event1.Subject = item.name;
            //    event1.Color = Color.Red;
            //    CalendarInlineEvents.Add(event1);

            //}
            #endregion
        }
        async void FillApsAsync(string Auth)
        {

        

                  Auth = "Bearer " + Auth;

            List<Appointment> appointments = new List<Appointment>();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization
                  = new AuthenticationHeaderValue("Bearer", Auth);
          
                var apiResponse = await client.GetStringAsync("https://timebookingapi.azurewebsites.net/api/timebooking");
          

            appointments = JsonConvert.DeserializeObject<List<Appointment>>(apiResponse);
            foreach (var item in appointments)
            {
                Appointment booking = new Appointment() { startDateTime = item.startDateTime, endDateTime = item.endDateTime, name = item.name, id = item.id };
                appointments.Add(booking);
            }

            foreach (var item in appointments)
            {
                CalendarInlineEvent event1 = new CalendarInlineEvent();
                event1.StartTime = item.startDateTime;
                event1.EndTime = item.endDateTime;
                event1.Subject = item.name;
                event1.Color = Color.Red;
                CalendarInlineEvents.Add(event1);

                }
            }

        }

    }
    }


