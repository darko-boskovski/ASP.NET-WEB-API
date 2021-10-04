using System;
using System.Collections.Generic;
using System.Text;
using WebApi_Workshop.Models;
using WebApi_Workshop_Class05.Models;

namespace WebApi_Workshop.Services.Interfaces
{
    public interface IMovieService
    {
        List<MovieModel> GetAllMovies();
        MovieModel GetMovieById(int id);
        void AddMovie(MovieModel movieModel);
        void UpdateMovie(MovieModel movieModel);
        void DeleteMovie(int id);
    }
}
