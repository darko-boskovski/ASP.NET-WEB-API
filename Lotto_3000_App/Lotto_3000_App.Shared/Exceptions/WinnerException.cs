using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto_3000_App.Shared.Exceptions
{
    public class WinnerException :Exception
    {
        public WinnerException(string message) : base(message)
        {

        }
    }
}
