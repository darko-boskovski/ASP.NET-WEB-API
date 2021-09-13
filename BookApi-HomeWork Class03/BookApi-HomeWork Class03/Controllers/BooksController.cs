using BookApi_HomeWork_Class03.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookApi_HomeWork_Class03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // GET: api/<BooksController>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return StatusCode(StatusCodes.Status200OK, StaticDB.Books);
        }

        // GET api/<BooksController>/5
        [HttpGet("{index}")]
        public ActionResult<string> Get(int index)
        {
            try
            {
                if (index < 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Index cannot be lower than zero.");
                }

                if (index >= StaticDB.Books.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "There is no such book");
                }

                return StatusCode(200, StaticDB.Books[index]);
            }
            catch
            {
                return StatusCode
            (StatusCodes.Status500InternalServerError,
            "Server Error");
            }

        }

        [HttpGet("BookQuery")]
        public ActionResult<Book> GetMultipleQueryParams(string title, string author)
        {
            try
            {
                if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(author))
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                if (!string.IsNullOrEmpty(title))
                {
                    Book book = StaticDB.Books.FirstOrDefault(x => x.Title.Contains(title));
                    if (book == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Book not found");
                    }
                    return StatusCode(StatusCodes.Status200OK, book);
                }

                if (!string.IsNullOrEmpty(author))
                {
                    Book book = StaticDB.Books.FirstOrDefault(x => x.Author.Equals(author));
                    if (book == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Book not found");
                    }
                    return StatusCode(StatusCodes.Status200OK, book);
                }

                Book bookDb = StaticDB.Books.FirstOrDefault(x => x.Title.Equals(title) && x.Author.Equals(author));

                if (bookDb == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Book not found");
                }
                return StatusCode(StatusCodes.Status200OK, bookDb);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

        [HttpPost("postBook")] //POST api/books/postBook
        public IActionResult PostBook([FromBody] Book book)
        {
            try
            {
                StaticDB.Books.Add(book);
                return StatusCode(StatusCodes.Status201Created, "Book added!");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured");
            }
        }

        [HttpPost("postBookTitle")] //POST api/books/postBook
        public IActionResult PostBook([FromBody] List<Book> books)
        {
            try
            {
                List<string> bookTitles = new List<string>();

                foreach (var item in books)
                {
                    bookTitles.Add(item.Title);
                }
            
                return StatusCode(StatusCodes.Status201Created, bookTitles);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured");
            }
        }



        // POST api/<BooksController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
