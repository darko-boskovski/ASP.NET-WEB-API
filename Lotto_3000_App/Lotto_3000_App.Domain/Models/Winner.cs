using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto_3000_App.Domain.Models
{
    public class Winner : BaseEntity
    {
        public string Fullname { get; set; }

        public IEnumerable<string> TicketCombination { get; set; }
        public int Prize { get; set; }
        public int SessionId { get; set; }
        public Session Session { get; set; }
    }
}
