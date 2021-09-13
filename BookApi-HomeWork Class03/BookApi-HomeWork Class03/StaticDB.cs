using BookApi_HomeWork_Class03.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi_HomeWork_Class03
{
    public static class StaticDB
    {
        public static List<Book> Books = new List<Book>
        {
          new Book()
          {
              Title = "Harry Potter",
              Author = "J.K. Rowling"
          },
             new Book()
          {
              Title = "Good Company",
              Author = "Cynthia D'Aprix Sweeney"
          },
                new Book()
          {
              Title = "Girls with bright future",
              Author = "Tracy Dobmeier and Wendy Katzman"
          },
                   new Book()
          {
              Title = "The Dutch House",
              Author = "Ann Patchett"
          },
                      new Book()
          {
              Title = "Normal People",
              Author = "Sally Rooney"
          },
                    new Book()   
                    {
              Author= "Samuel Beckett",
              Title= "Molloy, Malone Dies, The Unnamable, the trilogy"

           },
                    new Book() {
              Author="Giovanni Boccaccio",
              Title= "The Decameron"

            },
            new Book() {
             Author= "Jorge Luis Borges",
             Title= "Ficciones"

            }
        };
    }
}
