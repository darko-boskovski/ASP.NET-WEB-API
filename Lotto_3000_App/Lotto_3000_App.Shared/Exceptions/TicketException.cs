using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto_3000_App.Shared.Exceptions
{
    public class TicketException : Exception
    {
        public TicketException(string message) : base(message)
        {

        }
    }
}
