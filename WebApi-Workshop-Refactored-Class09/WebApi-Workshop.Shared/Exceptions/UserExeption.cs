using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi_Workshop.Shared.Exceptions
{
    public class UserException : Exception
    {
        public UserException(string message) : base(message)
        {

        }
    }
}
