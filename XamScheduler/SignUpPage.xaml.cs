using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamScheduler.Model;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;

namespace XamScheduler
{

    public partial class SignUpPage : ContentPage
    {
        public string Auth { get; set; }
        public SignUpPage()
        {
            InitializeComponent();
        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            var user = new User()
            {

                Password = passwordEntry.Text,
                ConfirmPassword = ConfirmPasswordEntry.Text,
                Email = emailEntry.Text
            };



            var signUpSucceeded = AreDetailsValid(user);
            if (signUpSucceeded)
            {
                var content = JsonConvert.SerializeObject(user);

                using (HttpClient client = new HttpClient())
                {

                    StringContent scontent = new StringContent(content.ToString(), Encoding.UTF8, "application/json");
                    await client.PostAsync("https://timebooking.azurewebsites.net/api/account/register", scontent);
                                  
                }
                //Assume it works and Log in

                var login = new LoginModel
                {
                    Username = emailEntry.Text,
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
                    ConfirmPasswordEntry.Text = string.Empty;
                }
            }
            else
            {
                messageLabel.Text = "Sign up failed";
            }
        }

            bool AreDetailsValid(User user)
        {
            if (ValidatePassword(user.Password))
            {
                return (user.Password == user.ConfirmPassword && !string.IsNullOrWhiteSpace(user.Email) && user.Email.Contains("@"));
            }
            return false;
            }
        private static bool ValidatePassword(string password)
        {
            return string.IsNullOrWhiteSpace(password)
              || password.Length < 6
              || password.Any(char.IsDigit)
              || password.Any(char.IsUpper) && password.Any(char.IsLower);
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
                        DisplayAlert("Error!", "Error!!", "Ok");
                    }
                }
        }
    } 
}



