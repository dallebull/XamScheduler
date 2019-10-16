using System;
using System.Collections.Generic;
using System.Text;

namespace XamScheduler.Model
{
    class LoginModel
    {
        public string grant_type = "password";
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

