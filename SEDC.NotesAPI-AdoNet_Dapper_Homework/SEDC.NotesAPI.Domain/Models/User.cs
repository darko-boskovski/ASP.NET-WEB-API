using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.NotesAPI.Domain.Models
{
    public class User
    {
        public int Id { get; set; } // recognizes Id as key (PK)
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }
        public int Age { get; set; }
        //public List<Note> Notes { get; set; }
    }
}
