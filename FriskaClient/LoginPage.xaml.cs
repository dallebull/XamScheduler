using System;
using FriskaClient.Model;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace FriskaClient
{

    public partial class LoginPage : ContentPage
    {

        static string url = App.url + "Token";
        public LoginPage()
        {
      
            InitializeComponent();
            try
            {
                EmailnameEntry.Text = FriskaClient.Services.Settings.LastUsedEmail;
                passwordEntry.Text = FriskaClient.Services.Settings.LastUsedPassword;

                if (EmailnameEntry.Text.Length != 0 && passwordEntry.Text.Length != 0)
                {
                    var login = new LoginModel
                    {
                        Username = EmailnameEntry.Text,
                        Password = passwordEntry.Text,
                    };

                     Login(login);
                }
            }
            catch (Exception)
            {
                EmailnameEntry.Text = null;
            }

            EmailnameEntry.Completed += (sender, args) => { passwordEntry.Focus(); };
            passwordEntry.Completed += (sender, args) => { OnLoginButtonClicked(null,null); };
            
          
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


        }
          

        async Task Login(LoginModel login)
        {


            var content = JsonConvert.SerializeObject(login);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);

                var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(dictionary) };
                var res = await client.SendAsync(req);

            if (res.IsSuccessStatusCode)
            {

          
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
                catch (Exception)
                {
                    await DisplayAlert("Error!","Error!!", "Ok");
                }
            }
            else
            {
                try
                {
                    var ex = ApiException.CreateApiException(res);
                    if (ex.Errors.Count() == 1)
                    {
                        await DisplayAlert("Oh No!", ex.Errors.FirstOrDefault().ToString(), "Ok");
                    }
                    else
                    {
                        for (int i = 0; i < ex.Errors.Count(); i++)
                        {
                            await DisplayAlert("Oh No!", ex.Errors.ElementAt(i).ToString(), "Ok");
                        }

                    }

                }
                catch (Exception)
                {

                    await DisplayAlert("Oh No!", "Något allvarligt gick fel!", "Ok");
                }
            }

        }
     
              
    }
}

