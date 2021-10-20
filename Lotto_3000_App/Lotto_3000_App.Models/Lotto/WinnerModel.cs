using Lotto_3000_App.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto_3000_App.Models.Lotto
{
    public class WinnerModel
    {

        //public int Id { get; set; }
        public int SessionId { get; set; }
        public IEnumerable<string> SessionCombination { get; set; }

        public string Fullname { get; set; }
        public IEnumerable<string> TicketCombination { get; set; }

        //public Prize Prize { get; set; }
        public string WiningPrize { get; set; }


        public static string SetPrize(int count)
        {
            switch (count)
            {
                case 1:
                    return "Won a Car";
                case 2:
                    return "Won a Vacation";
                case 3:
                    return "Won a Tv";
                case 4:
                    return "Won 100 dollars GiftCard";
                case 5:
                    return "Won 100 dollars GiftCard";
                default:
                    return "Better Luck next time";

            };
        }

    }
}
