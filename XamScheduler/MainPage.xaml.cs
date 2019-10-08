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
        public string Auth { get; set; }
        public MainPage()
        {           
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

      public void Handle_InlineToggled(object sender, InlineToggledEventArgs e)
        {
            if (e.selectedAppointment == null)
            {
                return;
            }
            
        }
        
        async void OnAlertYesNoClicked(object sender)
        {
            
            var tmpDate = calendar.SelectedDate.ToString();
            var tmpDate2 = DateTime.Parse(tmpDate);
            var DateDate = tmpDate2.Date.ToString("yyyy/MM/dd");
            bool answer = await DisplayAlert(DateDate, "Would you like to Book this Day", "Yes", "No");
            if (answer == true)
            {
                Navigation.PushAsync( new BookEvent((DateTime.Parse(calendar.SelectedDate.ToString())), Auth));           

            }
        }
      
    }
}