using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto_3000_App.Models.Lotto
{
    public class SessionModel
    {

        public int Id { get; set; }
        public IEnumerable<string> WinningCombination { get; set; }
        public DateTime TimeCreated { get; set; }
        public bool NotActive { get; set; }

        public SessionModel()
        {
            TimeCreated = DateTime.Now;
            NotActive = false;
        }


    }
}
