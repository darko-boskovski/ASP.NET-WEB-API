using System;
using System.Collections.Generic;
using System.Text;
using WebApi_Workshop.Domain.Models;
using WebApi_Workshop_Class05.Models;

namespace WebApi_Workshop.Models
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public Genre Genre { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string UserFullName { get; set; }
    }
}
