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
        static string url = App.url + "api/Account/Register";
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
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (usersender, cert, chain, sslPolicyErrors) => { return true; };
                HttpClient client = new HttpClient(clientHandler);

                StringContent scontent = new StringContent(content.ToString(), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, scontent);
                if (response.IsSuccessStatusCode)
                {
                    var login = new LoginModel
                    {
                        Username = user.Email,
                        Password = user.Password,
                    };

                    await Login(login);

                    //Navigation.InsertPageBefore(new LoginPage(), this);
                    //await Navigation.PopAsync();
                }
                else
                {
                    var ex = CreateApiException(response);
                    await DisplayAlert("Oh No!", ex.ToString(), "Ok");
                }
            }
            else
            {
                await DisplayAlert("Oh No!", "Check your Details", "Ok");
            }
        }

        bool AreDetailsValid(User user)
        {
            if (ValidatePassword(user.Password))
            {
                return (user.Password == user.ConfirmPassword &&
                    !string.IsNullOrWhiteSpace(user.Email) &&
                    user.Email.Contains("@") &&
                    !string.IsNullOrWhiteSpace(user.Namn) &&
                    !string.IsNullOrWhiteSpace(user.PhoneNumber) &&
                    user.PhoneNumber.Count() >= 10 &&
                    !string.IsNullOrWhiteSpace(user.Gender) &&
                    (user.Age >= 1900 && user.Age <= 2020));
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

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);

            var req = new HttpRequestMessage(HttpMethod.Post, App.url + "token") { Content = new FormUrlEncodedContent(dictionary) };
            var res = await client.SendAsync(req);

            var reqcontent = res.Content;
            string jsonContent = reqcontent.ReadAsStringAsync().Result;
            try
            {
                Token token = JsonConvert.DeserializeObject<Token>(jsonContent);
                App.Auth = token.access_token;

                var tmpName = token.userName.Split('@');
                var Name = tmpName[0];

                if (Name.Length > 1)
                {
                    Name = char.ToUpper(Name[0]) + Name.Substring(1);
                }
                App.User = Name;
            }
            catch (Exception)
            {
                await DisplayAlert("Error!", "Error!!", "Ok");
            }

            if (App.Auth != null)
            {
                App.IsUserLoggedIn = true;
                Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error!", "Error!!", "Ok");
                Navigation.InsertPageBefore(new LoginPage(), this);
                await Navigation.PopAsync();

            }
        }

        public static ApiException CreateApiException(HttpResponseMessage response)
        {
            var httpErrorObject = response.Content.ReadAsStringAsync().Result;

            // Create an anonymous object to use as the template for deserialization:
            var anonymousErrorObject =
                new { message = "", ModelState = new Dictionary<string, string[]>() };

            // Deserialize:
            var deserializedErrorObject =
                JsonConvert.DeserializeAnonymousType(httpErrorObject, anonymousErrorObject);

            // Now wrap into an exception which best fullfills the needs of your application:
            var ex = new ApiException(response);

            // Sometimes, there may be Model Errors:
            if (deserializedErrorObject.ModelState != null)
            {
                var errors =
                    deserializedErrorObject.ModelState
                                            .Select(kvp => string.Join(". ", kvp.Value));
                for (int i = 0; i < errors.Count(); i++)
                {
                    // Wrap the errors up into the base Exception.Data Dictionary:
                    ex.Data.Add(i, errors.ElementAt(i));
                }
            }
            // Othertimes, there may not be Model Errors:
            else
            {
                var error =
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(httpErrorObject);
                foreach (var kvp in error)
                {
                    // Wrap the errors up into the base Exception.Data Dictionary:
                    ex.Data.Add(kvp.Key, kvp.Value);
                }
            }
            return ex;
        }
    }



}
