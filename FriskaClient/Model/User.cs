using System;
using System.Collections.Generic;
using System.Text;

namespace FriskaClient.Model
{

    public class User
    {
        public string Email { get; set; }

        public string Gender { get; set; }

        public string CorrectAnswers { get; set; } = "0";

        public int Age { get; set; }
        public string PhoneNumber { get; set; }

        public string Password { get; set; }
        public string Namn { get; set; }
        public string ConfirmPassword { get; set; }



        public DateTime JoinDate { get; set; } = DateTime.Now.Date;
    }
}



