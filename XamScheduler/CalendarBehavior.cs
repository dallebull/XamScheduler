using System;
using System.Collections.Generic;
using System.Text;
    using Syncfusion.SfCalendar.XForms;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Xamarin.Forms;

namespace XamScheduler
{

 
        class CalendarBehavior : Behavior<ContentPage>
        {
          public  SfCalendar calendar;

            protected override void OnAttachedTo(ContentPage bindable)
            {
                base.OnAttachedTo(bindable);
                calendar = bindable.FindByName<SfCalendar>("calendar");
                calendar.OnMonthCellLoaded += Calendar_OnMonthCellLoaded;
            }

        private void Calendar_OnMonthCellLoaded(object sender, MonthCellLoadedEventArgs e)
        {
            List<DateTime> blackoutDates = new List<DateTime>();
            var dayOfWeek = e.Date.DayOfWeek;
            if (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday)
            {
                blackoutDates.Add(e.Date);
                calendar.BlackoutDates = blackoutDates;
            }
        }


    }
    }

