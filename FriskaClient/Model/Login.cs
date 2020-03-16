using System;
using System.Collections.Generic;
using System.Text;

namespace FriskaClient.Model
{
    class Login
    {
        public string grant_type = "password";
        private string _username { get; set; }
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    FriskaClient.Services.Settings.LastUsedEmail = value;
                }
            }
        }
        public string _password { get; set; }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    FriskaClient.Services.Settings.LastUsedPassword = value;
                }
            }
        }
    }
}

