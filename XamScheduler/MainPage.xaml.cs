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
        public DateTime Date { get; set; }
        public MainPage()
        {         
            InitializeComponent();

            ToolbarItem item = new ToolbarItem
            {
                Text = App.User,
            };
            this.ToolbarItems.Add(item);
            item.Clicked += OnLogout;

        }

   

        public async void OnDateCellHolding(object sender, Syncfusion.SfCalendar.XForms.DayCellHoldingEventArgs e)
        {
           BookEventQuery(calendar.SelectedDate);
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
                    RemoveEvent(id);
                }
            }
            else
            {
                await DisplayAlert("Allready Taken", appointment.StartTime.ToString("yyyy-MM-dd HH:mm"), "Ok");
            }
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
                await Navigation.PushAsync(new BookEvent((DateTime.Parse(calendar.SelectedDate.ToString()))));
            }
        }

        public async void RemoveEvent(int id)
        {
            bool answer = await DisplayAlert("Remove Booking", "Are you 110% Sure?", "Yes", "No");
            if (answer)
            {
                try
                {


                    using (HttpClient client = new HttpClient())
                    {
                        var url = "https://timebooking.azurewebsites.net/api/timebooking/" + id;

                        var DelAuth = new Dictionary<string, string>();
                        DelAuth.Add("Authorization", "Bearer " + App.Auth);
                        DelAuth.Add("Content-Type", "application/x-www-form-urlencoded");
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.Auth);
                        var req = new HttpRequestMessage(HttpMethod.Delete, url) { Content = new FormUrlEncodedContent(DelAuth) };
                        var res = await client.SendAsync(req);

                        if (res.IsSuccessStatusCode)
                        {
                            Navigation.InsertPageBefore(new MainPage(), this);
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert("Error!", "Could not Remove Booking!!", "Ok");
                            Navigation.InsertPageBefore(new MainPage(), this);
                            await Navigation.PopAsync();
                        }
                    }
                }
                catch (Exception)
                {
                    await DisplayAlert("Error!", "My Name is Error!!", "Hello");
                    Navigation.InsertPageBefore(new MainPage(), this);
                    await Navigation.PopAsync();
                }
            }
        }
       static void OnLogout(object sender, EventArgs e)
        {
            
            App.IsUserLoggedIn = false;
            App.Auth = string.Empty;
            App.User = string.Empty;
            App.Devmode = false;
            App.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}

