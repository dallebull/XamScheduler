using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FriskaClient.Model
{
    class UsersViewModel
    { 
        static public string url = App.url + "api/AdminApi/GetUsers";
        static public string aurl = App.url + "api/Account/IsAdmin";
    


    private ObservableCollection<User> _allUsers = new ObservableCollection<User>();

    public ObservableCollection<User> AllUsers { get { return _allUsers; } }
    public UsersViewModel()
        {

            LoadUsers(App.Auth);
        }

        public async void LoadUsers(string Auth)
        {

            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };

                // Pass the handler to httpclient(from you are calling api)
                HttpClient client = new HttpClient(clientHandler);


                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Auth);
            
             
                var apiResponse = await client.GetStringAsync(url);


                var user = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                var Now = DateTime.Now.Year;
                foreach (var item in user)
                {
                   
                  
                    User usr = new User
                    {
            

                    Age =  Now- item.Age,
                    CorrectAnswers = item.CorrectAnswers.ToString(),
                    Email = item.Email,
                    Gender = item.Gender,        
                        JoinDate = item.JoinDate,
                    PhoneNumber = item.PhoneNumber,
                    Namn = item.Namn,
                        EmailConfirmed = item.EmailConfirmed

                };
                    var myurl = aurl + "?Id=" + item.Email;
                    StringContent scontent = new StringContent("?Id=" + item.Email, Encoding.UTF8, "application/json");
                    var roleAnswer = await client.PostAsync(myurl, scontent);
                    if (roleAnswer.IsSuccessStatusCode)
                    {
                        usr.IsAdmin = true;
                    }
                    else
                    {
                        usr.IsAdmin = false;
                    }
                    _allUsers.Add(usr);
                
                }
             
            }
            catch (Exception )
            {

             

            }

        }

    }
}
