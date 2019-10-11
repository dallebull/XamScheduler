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
                        bool answer = await DisplayAlert("Delete", "Would you like to Remove your Booking for this Day", "Yes", "No");
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
            bool answer = await DisplayAlert("Delete", "Are you 110% Sure?", "Yes", "No");
            if (answer)
            {
                try
                {

                    var content = JsonConvert.SerializeObject(Auth);
                    var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);

                    using (HttpClient client = new HttpClient())
                    {
                        var url =
    "https://timebooking.azurewebsites.net/api/timebooking/" + (e.SelectedAppointment as CustomAppointment).Id;

                        var req = new HttpRequestMessage(HttpMethod.Delete, url) { Content = new FormUrlEncodedContent(dictionary) };
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

