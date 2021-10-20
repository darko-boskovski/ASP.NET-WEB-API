using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lotto_3000_App.Domain.Models
{
    public class Ticket : BaseEntity
    {
        //public int Id { get; set; }
     
        public int SessionId { get; set; }
        public IEnumerable<string> TicketCombination { get; set; } 
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
