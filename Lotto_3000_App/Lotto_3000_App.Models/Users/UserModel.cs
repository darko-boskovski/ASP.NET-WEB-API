using Lotto_3000_App.Models.Lotto;
using Lotto_3000_App.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto_3000_App.Models.Users
{
    public class UserModel
    {
        public UserModel()
        {
            Role = Role.Player;
        }
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public List<TicketModel> Tickets { get; set; } = new List<TicketModel>();
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public Role Role { get; set; }

    }
}
