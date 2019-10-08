using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamScheduler.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;

namespace XamScheduler
{

        public partial class SignUpPage : ContentPage
        {
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
                                                

     
                   // Sign up logic goes here

            var signUpSucceeded = AreDetailsValid(user);
                if (signUpSucceeded)
                {
                var content = JsonConvert.SerializeObject(user);

                using (HttpClient client = new HttpClient())
                {

                    StringContent scontent = new StringContent(content.ToString(), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("https://timebooking.azurewebsites.net/api/account/register", scontent);
                    
                //Login
             

                }

                var rootPage = Navigation.NavigationStack.FirstOrDefault();
                    if (rootPage != null)
                    {
                        App.IsUserLoggedIn = true;
                        Navigation.InsertPageBefore(new MainPage(), Navigation.NavigationStack.First());
                        await Navigation.PopToRootAsync();
                    }
                }
                else
                {
                    messageLabel.Text = "Sign up failed";
                }
            }

            bool AreDetailsValid(User user)
            {
                return (!string.IsNullOrWhiteSpace(user.Password) && !string.IsNullOrWhiteSpace(user.ConfirmPassword) && user.Password == user.ConfirmPassword && !string.IsNullOrWhiteSpace(user.Email) && user.Email.Contains("@"));
            }
        }
    }


