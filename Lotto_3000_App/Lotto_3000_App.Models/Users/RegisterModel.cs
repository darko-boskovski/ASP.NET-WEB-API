using Lotto_3000_App.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto_3000_App.Models.Users
{
    public class RegisterModel
    {
        public RegisterModel()
        {
            Role = Role.Player;
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int TicketId { get; set; }
        public Role Role { get; set; }
        public string ConfirmedPassword { get; set; }

    }
}

