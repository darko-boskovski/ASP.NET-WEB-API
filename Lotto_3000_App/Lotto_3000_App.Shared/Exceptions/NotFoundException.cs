using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto_3000_App.Shared.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {

        }
    }
}
