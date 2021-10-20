using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto_3000_App.Domain.Models
{
    public class User : BaseEntity
    {
        //public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
