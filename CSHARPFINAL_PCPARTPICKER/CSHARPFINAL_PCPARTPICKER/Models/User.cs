using System;
using System.Collections.Generic;
using System.Text;

namespace CSHARPFINAL_PCPARTPICKER.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsEmployee { get; set; }
        public List<Part> Cart { get; set; }
        public List<Order> OrderHistory{ get; set; }

        public User(string username, string password, bool isEmployee)
        {
            Username = username;
            Password = password;
            IsEmployee = isEmployee;
            Cart = new List<Part>();
            OrderHistory = new List<Order>();
        }
    }
}
