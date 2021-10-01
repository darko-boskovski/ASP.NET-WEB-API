using System;
using System.Collections.Generic;
using System.Text;
using WebApi_Workshop_Class05.Models;

namespace WebApi_Workshop.Domain.Models
{
   public class User
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
