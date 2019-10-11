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
       

        public MainPage()
        {
            App.Auth = Auth;

                InitializeComponent();

        }
        public MainPage(string Auth)
        {
            this.Auth = Auth;
            InitializeComponent();       
            
        }

        public async void OnDateCellHolding(object sender, EventArgs e)
        {
        
                OnAlertYesNoClicked(calendar.SelectedDate);

      
        }

        async void Handle_InlineToggled(object sender, InlineToggledEventArgs e)
        {
            if ((e.SelectedAppointment as CalendarEventCollection).Count != 0)
            {
                foreach (var item in e.SelectedAppointment as CalendarEventCollection)
                {
                    if (item.Subject != null || item.Subject != string.Empty)
                    {
                        bool answer = await DisplayAlert("", "Would you like to Remove your booking for this Day", "Yes", "No");
                        if (answer)
                        {
                            RemoveEvent(e, Auth);
                        }                      
                    }
                }
            }

     

        }

        async void OnAlertYesNoClicked(object sender)
        {
            

            var tmpDate = calendar.SelectedDate.ToString();
            var tmpDate2 = DateTime.Parse(tmpDate);
            var DateDate = tmpDate2.Date.ToString("yyyy-MM-dd");
            bool answer = await DisplayAlert(DateDate, "Would you like to Book this Day", "Yes", "No");
            if (answer == true)
            {
                Navigation.PushAsync( new BookEvent((DateTime.Parse(calendar.SelectedDate.ToString())), Auth));           

            }
        }

        public async void RemoveEvent(InlineToggledEventArgs e, string Auth)
        {
            string errormsg = "if i could, the booking would be removed now";
            DisplayAlert("Void" , errormsg , "Ok");
        }


    }
}
