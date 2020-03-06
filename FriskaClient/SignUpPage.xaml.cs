using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FriskaClient.Model;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;

namespace FriskaClient
{

    public partial class SignUpPage : ContentPage
    {
            public SignUpPage()
        {
            InitializeComponent();        
            userEntry.Completed += (sender, args) => { emailEntry.Focus(); };
            emailEntry.Completed += (sender, args) => { genderEntry.Focus(); };
            genderEntry.Completed += (sender, args) => { numberEntry.Focus(); };
            numberEntry.Completed += (sender, args) => { passwordEntry.Focus(); };
            passwordEntry.Completed += (sender, args) => { confirmPasswordEntry.Focus(); };
            confirmPasswordEntry.Completed += (sender, args) => { OnSignUpButtonClicked(null, null); };
        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            var user = new User()
            {
                Email = emailEntry.Text,
                Namn = userEntry.Text,
                Gender = genderEntry.Text,
                Age = Int32.Parse(ageEntry.Text),
                PhoneNumber = numberEntry.Text,
                Password = passwordEntry.Text,
                ConfirmPassword = confirmPasswordEntry.Text
            };



            var signUpSucceeded = AreDetailsValid(user);
            if (signUpSucceeded)
            {
                var content = JsonConvert.SerializeObject(user);

                using (HttpClient client = new HttpClient())
                {

                    StringContent scontent = new StringContent(content.ToString(), Encoding.UTF8, "application/json");
                    await client.PostAsync("Https://localhost:44349/api/accountapi/register", scontent);
                                  
                }
                //Assume it works and Log in

                var login = new LoginModel
                {
                    Username = emailEntry.Text,
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
                    confirmPasswordEntry.Text = string.Empty;
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
                return (user.Password == user.ConfirmPassword && !string.IsNullOrWhiteSpace(user.Email) && user.Email.Contains("@") && !string.IsNullOrWhiteSpace(user.Namn) && !string.IsNullOrWhiteSpace(user.PhoneNumber) && !string.IsNullOrWhiteSpace(user.Gender) && (user.Age >=1900 && user.Age <= 2020));
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
                        App.Auth = token.access_token;
                        App.User = token.displayName;
                }
                    catch (Exception)
                    {
                        await DisplayAlert("Error!", "Error!!", "Ok");
                    }
                }
        }
    } 
}



