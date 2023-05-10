using System;
using System.Collections.Generic;
using System.Text;

namespace mobile.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginModel(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
