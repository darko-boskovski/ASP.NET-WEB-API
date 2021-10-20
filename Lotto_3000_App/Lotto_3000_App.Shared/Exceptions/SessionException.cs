using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto_3000_App.Shared.Exceptions
{
    public class SessionException : Exception
    {
        public SessionException(string message) : base(message)
        {

        }
    }
}
