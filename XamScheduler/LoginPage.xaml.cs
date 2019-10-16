using System;
using XamScheduler.Model;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamScheduler
{

    public partial class LoginPage : ContentPage
    {
        public string Auth { get; set; }
   
        public LoginPage()
        {
            App.Devmode = true;  //Set this to True to Auto Login (Change Email an PW in method)
            InitializeComponent();  

            EmailnameEntry.Completed += (sender, args) => { passwordEntry.Focus(); };
            passwordEntry.Completed += (sender, args) => { OnLoginButtonClicked(null,null); };

            if (App.Devmode)
            {
                DevLogin();
            }          
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
            if (App.Auth != null)
            {
                App.IsUserLoggedIn = true;
                    Navigation.InsertPageBefore(new MainPage(), this);
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
                    App.Auth = token.access_token;
                    App.User = token.displayName;
                }
                catch (Exception)
                {
                    DisplayAlert("Error!","Error!!", "Ok");                   
                }           
            }
        }
        async void DevLogin()
        {
            var login = new LoginModel
            {
                Username = "admin@admin.com",
                Password = "Test1234!",
            };

            await Login(login);
            if (App.Auth != null)
            {               
                App.IsUserLoggedIn = true;
                Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync();
            }
            else
            {
                messageLabel.Text = "Dev Login failed";
                passwordEntry.Text = string.Empty;
            }

        }
    }
}

