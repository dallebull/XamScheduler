using Syncfusion.SfCalendar.XForms;
using Xamarin.Forms;

namespace FriskaClient
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
            //List<DateTime> blackoutDates = new List<DateTime>();
            //var dayOfWeek = e.Date.DayOfWeek;
            //if (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday)
            //{
            //    blackoutDates.Add(e.Date);
            //    calendar.BlackoutDates = blackoutDates;
            //}

            //Uncomment to block weekends.
        }


    }
    }

