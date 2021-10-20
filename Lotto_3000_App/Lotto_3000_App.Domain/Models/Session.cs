using Lotto_3000_App.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lotto_3000_App.Domain
{
    public class Session : BaseEntity
    {

        //public int Id { get; set; }

      
        public IEnumerable<string> WinningCombination { get; set; } 
        public DateTime TimeCreated { get; set; }
        public bool NotActive { get; set; }
        public List<Winner> Winners { get; set; }
    }
}
