using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_Workshop.Models;
using WebApi_Workshop.Services.Implementations;
using WebApi_Workshop.Services.Interfaces;
using WebApi_Workshop.Shared.Exceptions;
using WebApi_Workshop_Class05.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_Workshop_Class05.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        private IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: api/movies
        [HttpGet]
        public ActionResult<Movie> Get()
        {
           
                try
                {
                    return StatusCode(StatusCodes.Status200OK, _movieService.GetAllMovies());
                }
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            
        }

        // GET api/movies/5
        [HttpGet("{id}")]
        public ActionResult<Movie> Get(int id)
        {


            try
            {
                return StatusCode(StatusCodes.Status200OK, _movieService.GetMovieById(id));
            }
            catch (NotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }

        }


        [HttpGet("genre")]
        public ActionResult<List<Movie>>GetByGenre(string genre)
        {
            try
            {
                if (string.IsNullOrEmpty(genre))
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
         
               List<MovieModel> movieList = (_movieService.GetAllMovies().Where(x => x.Genre.ToString().ToLower().Contains(genre)).ToList());
                    if (movieList == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "There is no movie by that genre");
                    }
                    return StatusCode(StatusCodes.Status200OK, movieList);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/movies
        [HttpPost]
        public IActionResult Post([FromBody] MovieModel movieModel)
        {
            try
            {
                _movieService.AddMovie(movieModel);
                return StatusCode(StatusCodes.Status201Created, "Movie Created");
            }
            catch (NotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (MovieException e)
            {
                //log
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        // PUT api/movies/5
        [HttpPut]
        public IActionResult Put([FromBody] MovieModel movieModel)
        {
            try
            {
                _movieService.UpdateMovie(movieModel);
                return StatusCode(StatusCodes.Status204NoContent, "Movie updated");
            }
            catch (NotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (MovieException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        // DELETE api/movies/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _movieService.DeleteMovie(id);
                return StatusCode(StatusCodes.Status204NoContent, "Movie Deleted");
            }
            catch (NotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}
