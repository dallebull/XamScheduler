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
            //f = f as InlineToggledEventArgs;  //Funkar inteee


            //if ((f.SelectedAppointment as CalendarEventCollection).Count != 0)
            //{
            //    foreach (var item in f.SelectedAppointment as CalendarEventCollection)
            //    {
            //        if (item.Subject != "")
            //        {
            //            bool answer = await DisplayAlert(item.StartTime.ToString(), "Would you like to Remove this Booking", "Yes", "No");
            //            if (answer)
            //            {
            //                int id = (item as CustomAppointment).EventId;

            //                RemoveEvent(id, Auth);
            //            }
            //        }
            //    }
            //}
            OnAlertYesNoClicked(calendar.SelectedDate);

      
        }

        async void Handle_InlineToggled(object sender, InlineToggledEventArgs e)
        {
            if ((e.SelectedAppointment as CalendarEventCollection).Count != 0)
            {
                foreach (var item in e.SelectedAppointment as CalendarEventCollection)
                {
                    if (item.Subject != "")
                    {
                        bool answer = await DisplayAlert(item.StartTime.ToString(), "Would you like to Remove this Booking", "Yes", "No");
                        if (answer)
                        {
                            int id = (item as CustomAppointment).EventId;

                            RemoveEvent(id, Auth);
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

                          //Här blir det dock unauthorized. Men om man kopierar DelAuth[0].Value, postar in i Postman och byter länken till rätt ID så fungerar det i postman.
                        
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

