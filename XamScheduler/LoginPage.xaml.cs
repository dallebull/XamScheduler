using System;
using XamScheduler.Model;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Linq;
using System.Net;
using System.IO;
using System.Collections.Generic;
using Org.Json;
using Org.Apache.Http.Util;
using System.Threading.Tasks;

namespace XamScheduler
{

    public partial class LoginPage : ContentPage
    {
        public string Auth { get; set; }
        public LoginPage()
        {
            InitializeComponent();
        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var login = new LoginModel
            {
                Username = EmailnameEntry.Text,
                Password = passwordEntry.Text,

            };

           await Login(login);
            if (Auth != null)
            {
                App.Auth = Auth;
                App.IsUserLoggedIn = true;
                Navigation.InsertPageBefore(new MainPage(Auth), this);
                await Navigation.PopAsync();
            }
            else
            {
                messageLabel.Text = "Login failed";
                passwordEntry.Text = string.Empty;
            }
        }
          

        async Task Login(LoginModel login)
        {

            var content = JsonConvert.SerializeObject(login);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);

            using (HttpClient client = new HttpClient())
            {
               var req = new HttpRequestMessage(HttpMethod.Post, "https://timebooking.azurewebsites.net/token") { Content = new FormUrlEncodedContent(dictionary) };
                var res = await client.SendAsync(req);
              
                var reqcontent = res.Content;
                string jsonContent = reqcontent.ReadAsStringAsync().Result;
                try
                {
                    Token token = JsonConvert.DeserializeObject<Token>(jsonContent);
                    Auth = token.access_token;   
                }
                catch (Exception)
                {
                    DisplayAlert("Error!","Error!!", "Ok");
                   
                }
           
            }

        }
    }
}

