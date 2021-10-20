using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto_3000_App.Models.Lotto
{
    public  class TicketModel
    {
        //get this by token, when token validation is done
        public int UserId { get; set; }
        public int SessionId { get; set; }
        public IEnumerable<string> TicketCombination { get; set; }
    }
}
