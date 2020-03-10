using System;
using FriskaClient.Model;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriskaClient
{

    public partial class LoginPage : ContentPage
    {

        static string url = App.url +"Token";
        public LoginPage()
        {
            App.Devmode = false;  //Set this to True to Auto Login (Change Email an PW in method)
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
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);

            var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(dictionary) };
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
                    await DisplayAlert("Error!","Error!!", "Ok");                   
                }           
            
        }
        async void DevLogin()
        {
            var login = new LoginModel
            {
                Username = "Dallebull@hotmail.com",
                Password = "Abcd1234!",
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

