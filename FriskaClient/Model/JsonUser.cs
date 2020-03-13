using System;
using System.Collections.Generic;
using System.Text;

namespace FriskaClient.Model
{
  public class JsonUser
    {
        public class Role
        {
            public string UserId { get; set; }
            public string RoleId { get; set; }
        }

        public class RootObject
        {
    
            public List<Role> Roles { get; set; }
            public int CorrectAnswers { get; set; }
            public string Namn { get; set; }
            public int Age { get; set; }
            public string Gender { get; set; }
            public DateTime JoinDate { get; set; }
            public string Email { get; set; }
            public bool EmailConfirmed { get; set; }
            public string PasswordHash { get; set; }
            public string SecurityStamp { get; set; }
            public string PhoneNumber { get; set; }
            public bool PhoneNumberConfirmed { get; set; }
            public bool TwoFactorEnabled { get; set; }
            public object LockoutEndDateUtc { get; set; }
            public bool LockoutEnabled { get; set; }
            public int AccessFailedCount { get; set; }
            public string Id { get; set; }
            public string UserName { get; set; }
        }
    }
}
