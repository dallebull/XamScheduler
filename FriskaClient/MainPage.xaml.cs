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
using FriskaClient.Model;
using FriskaClient.Services;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FriskaClient
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<KontrollSvar> _myAnswers { get; set; } 

        static string url = App.url + "api/kontrollsvarsapi/";
        public MainPage()
        {         
            InitializeComponent();
            //FillApsAsync(App.Auth);
            var vm = new ViewModel();
            ansList.ItemsSource = vm.MyAnswers;
            _myAnswers = vm.MyAnswers;

             ToolbarItem item = new ToolbarItem
            {
                Text = App.User,
            };

            this.ToolbarItems.Add(item);
            item.Clicked += OnLogout;

        }

        async void OnAddButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new AddKontroll());
        }    
        async void OnScanButtonClicked(object sender, EventArgs args)
        {

            string Kontroll = null;
            string KontrollTag = null;
            try
            {
                var scanner = DependencyService.Get<IQrScanningService>();
             
                var result = await scanner.ScanAsync();
                if (result != null)
                {
                    Uri myUri = new Uri(result);
                    Kontroll = HttpUtility.ParseQueryString(myUri.Query).Get("Kontroll");
                    KontrollTag = HttpUtility.ParseQueryString(myUri.Query).Get("KontrollTag");
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            if (Kontroll != null && KontrollTag != null)
            {


                KontrollSvar ks = new KontrollSvar();

                ks.KontrollTag = KontrollTag;
                ks.Kontroll = Int32.Parse(Kontroll);

                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sslsender, cert, chain, sslPolicyErrors) => { return true; };

                // Pass the handler to httpclient(from you are calling api)
                HttpClient client = new HttpClient(clientHandler);

                //Put Answer on Site
                var content = JsonConvert.SerializeObject(ks);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.Auth);
                StringContent scontent = new StringContent(content.ToString(), Encoding.UTF8, "application/json");
                var apiAnswer = await client.PostAsync(url, scontent);
                if (apiAnswer.IsSuccessStatusCode)
                {
                  
                    Navigation.InsertPageBefore(new MainPage(), this);
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Oh No!", apiAnswer.StatusCode.ToString(), "Ok");
                }
            }
        }

        async void OnDelButtonClicked(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.ImageButton)sender;
            var Id = item.CommandParameter;
            var IdString = Id.ToString();
            var tmpkontroll = from a in _myAnswers where a.ID.ToString() == IdString select a;
            var kontroll = tmpkontroll.First() as KontrollSvar;

            var action = await DisplayAlert("Ta Bort?", "Vill du ta bort Kontrollen?\n" +kontroll.Kontroll + "  " + kontroll.KontrollTag, "Ja", "Nej");
            if (action)
            {
               

                HttpClientHandler clientHandler = new HttpClientHandler();
              clientHandler.ServerCertificateCustomValidationCallback = (thissender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
            HttpClient client = new HttpClient(clientHandler);


            client.Timeout = TimeSpan.FromMinutes(10);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.Auth);
          
                var apiAnswer = await client.DeleteAsync(url + Id);
                if (apiAnswer.IsSuccessStatusCode)
                {
             
                    Navigation.InsertPageBefore(new MainPage(), this);
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Oh No!", apiAnswer.StatusCode.ToString(), "Ok");
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
    //                var url = "https://31.208.194.94/api/Kontrollsvarsapi/" + id;

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
            App.Current.MainPage = new NavigationPage(new LoginPage())
            {

                BarBackgroundColor = Color.FromHex("#ed1c24"),
                BarTextColor = Color.White
            };
        }
    }
}

