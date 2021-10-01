using System;
using System.Collections.Generic;
using System.Text;
using WebApi_Workshop.Domain.Models;
using WebApi_Workshop.Models;
using WebApi_Workshop_Class05.Models;

namespace WebApi_Workshop.Mappers
{
    public static class MovieMapper
    {
        public static Movie ToMovie(this MovieModel movieModel)
        {
            return new Movie
            {
                Id = movieModel.Id,
                Title = movieModel.Title,
                Description = movieModel.Description,
                Year = movieModel.Year,
                Genre = movieModel.Genre,
                UserId = movieModel.UserId
            };
        }

        public static MovieModel ToMovieModel(this Movie movie)
        {
            return new MovieModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                Year = movie.Year,
                Genre = movie.Genre,
                UserId = movie.UserId
            };
        }

        public static Movie MapMovie(Movie movie, MovieModel movieModel, User user)
        {
            movie.Id = movieModel.Id;
            movie.Title = movieModel.Title;
            movie.Description = movieModel.Description;
            movie.Year = movieModel.Year;
            movie.Genre = movieModel.Genre;
            movie.UserId = movieModel.UserId;
            movie.User = user;

            return movie;

        }

    }
}
