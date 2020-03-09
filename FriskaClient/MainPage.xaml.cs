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
using FriskaClient.Model;
using System.Collections.ObjectModel;

namespace FriskaClient
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<KontrollSvar> myAnswers  = new ObservableCollection<KontrollSvar>();

        public ObservableCollection<KontrollSvar> MyAnswers { get { return myAnswers; } }

        public MainPage()
        {         
            InitializeComponent();
            //FillApsAsync(App.Auth);
            var vm = new ViewModel();
            ansList.ItemsSource = vm.MyAnswers;


             ToolbarItem item = new ToolbarItem
            {
                Text = App.User,
            };
            this.ToolbarItems.Add(item);
            item.Clicked += OnLogout;

        }


        async void FillApsAsync(string Auth)
        {

            var ansList = new ListView();
            ansList.ItemsSource = myAnswers;

            List<KontrollSvar> Answers = new List<KontrollSvar>();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Auth);
                var apiResponse = await client.GetStringAsync("http://31.208.194.94/api/kontrollsvarsapi/");
                
                 Answers = JsonConvert.DeserializeObject<List<KontrollSvar>>(apiResponse);

                foreach (var item in Answers)
                {
                    KontrollSvar ans = new KontrollSvar();
                    ans.ID = item.ID;
                    ans.UserID = item.UserID;
                    ans.Kontroll = item.Kontroll;
                    ans.KontrollTag = item.KontrollTag;
                    ans.RegDate = item.RegDate;
                    ans.Rattsvar = item.Rattsvar;
                    ans.YearID = item.YearID;



                    myAnswers.Add(ans);

                }
            }
        }
        //public async void OnDateCellHolding(object sender, Syncfusion.SfCalendar.XForms.DayCellHoldingEventArgs e)
        //{
        //   BookEventQuery(calendar.SelectedDate);
        //}
        //private async void Calendar_InlineItemTapped(object sender, InlineItemTappedEventArgs e)
        //{
        //    var appointment = e.InlineEvent as CustomAppointment;
        //    var id = appointment.EventId;
        //    if (appointment.Subject != "")
        //    {
        //        bool app = await DisplayAlert(appointment.Subject, appointment.StartTime.ToString("yyyy-MM-dd HH:mm"), "Ok", "Remove");
        //        if (!app)
        //        {
        //            RemoveEvent(id);
        //        }
        //    }
        //    else
        //    {
        //        await DisplayAlert("Allready Taken", appointment.StartTime.ToString("yyyy-MM-dd HH:mm"), "Ok");
        //    }
        //}



        //async void BookEventQuery(object sender)
        //{
        //    var tmpDate = calendar.SelectedDate.ToString();
        //    var tmpDate2 = DateTime.Parse(tmpDate);
        //    var DateDate = tmpDate2.Date.ToString("yyyy-MM-dd");
        //    bool answer = await DisplayAlert(DateDate, "Would you like to Book this Day", "Yes", "No");
        //    if (answer == true)
        //    {
        //        this.Date = (DateTime.Parse(calendar.SelectedDate.ToString()));
        //        await Navigation.PushAsync(new BookEvent((DateTime.Parse(calendar.SelectedDate.ToString()))));
        //    }
        //}

        //public async void RemoveEvent(int id)
        //{
        //    bool answer = await DisplayAlert("Remove Booking", "Are you 110% Sure?", "Yes", "No");
        //    if (answer)
        //    {
        //        try
        //        {


        //            using (HttpClient client = new HttpClient())
        //            {
        //                var url = "http://31.208.194.94/api/Kontrollsvarsapi/" + id;

        //                var DelAuth = new Dictionary<string, string>();
        //                DelAuth.Add("Authorization", "Bearer " + App.Auth);
        //                DelAuth.Add("Content-Type", "application/x-www-form-urlencoded");
        //                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.Auth);
        //                var req = new HttpRequestMessage(HttpMethod.Delete, url) { Content = new FormUrlEncodedContent(DelAuth) };
        //                var res = await client.SendAsync(req);

        //                if (res.IsSuccessStatusCode)
        //                {
        //                    Navigation.InsertPageBefore(new MainPage(), this);
        //                    await Navigation.PopAsync();
        //                }
        //                else
        //                {
        //                    await DisplayAlert("Error!", "Could not Remove Booking!!", "Ok");
        //                    Navigation.InsertPageBefore(new MainPage(), this);
        //                    await Navigation.PopAsync();
        //                }
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            await DisplayAlert("Error!", "My Name is Error!!", "Hello");
        //            Navigation.InsertPageBefore(new MainPage(), this);
        //            await Navigation.PopAsync();
        //        }
        //    }
        //}
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

