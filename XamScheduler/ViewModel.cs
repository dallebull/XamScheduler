
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Xamarin.Forms;
using XamScheduler.Model;

namespace XamScheduler
{
        public class ViewModel
        {

            public CalendarEventCollection CalendarInlineEvents { get; set; } = new CalendarEventCollection();

            public ViewModel()
            {
            // Create events 
            ApService ap = new ApService();
            var ApList = ap.FillAps();

            foreach (var item in ApList)
            {
                CalendarInlineEvent event1 = new CalendarInlineEvent();
                event1.StartTime = item.startDateTime;
                event1.EndTime = item.endDateTime;
                event1.Subject = item.name;
                event1.Color = Color.Fuchsia;
                CalendarInlineEvents.Add(event1);

            }

            // Add events into a CalendarInlineEvents collection
      
            }

    
    }
    }


