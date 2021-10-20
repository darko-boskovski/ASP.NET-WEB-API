using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto_3000_App.Shared.Exceptions
{
    public class UserException : Exception
    {
        public UserException(string message) : base(message)
        {

        }
    }
}
