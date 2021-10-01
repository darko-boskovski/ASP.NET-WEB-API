using System;
using System.Collections.Generic;
using System.Text;
using WebApi_Workshop.Domain.Models;
using WebApi_Workshop.Services.Interfaces;
using WebApi_Workshop_Class05.Models;
using WebApi_Workshop;
using WebApi_Workshop.DataAccess;
using WebApi_Workshop.Shared.Exceptions;
using WebApi_Workshop.Models;
using WebApi_Workshop.Mappers;

namespace WebApi_Workshop.Services.Implementations
{
    public class MovieService : IMovieService
    {
       
        private IRepository<Movie> _movieRepository;
        private IRepository<User> _userRepository;

        public MovieService(IRepository<Movie> movieRepository, IRepository<User> userRepository)
        {
            _movieRepository = movieRepository;
            _userRepository = userRepository;
        }

        public void AddMovie(MovieModel movieModel)
        {
            User userDb = _userRepository.GetById(movieModel.UserId);
            if (userDb == null)
            {
                throw new NotFoundException($"The user with Id {movieModel.UserId} was not found");
            }
            if (string.IsNullOrEmpty(movieModel.Title))
            {
                throw new MovieException("The property Title for movie is required");
            }
            if (movieModel.Title.Length > 100)
            {
                throw new MovieException("The property Title can't contain more then 100 characters");
            }
            if (movieModel.Year.ToString().Length > 10)
            {
                throw new MovieException("The property Year can't contain more then 10 characters");
            }
            if (movieModel.Id != 0)
            {
                throw new MovieException("Id must not be set!");
            }
            Movie movieForDb = movieModel.ToMovie();
            movieForDb.User = userDb;
            _movieRepository.Add(movieForDb);
        }

        public void DeleteMovie(int id)
        {
            Movie movieDb = _movieRepository.GetById(id);
            if (movieDb == null)
            {
                throw new NotFoundException($"The Movie with id {id} was not found");
            }
            _movieRepository.Delete(movieDb);
        }

        public List<MovieModel> GetAllMovies()
        {
            List<Movie> moviesDb = _movieRepository.GetAll();
            List<MovieModel> noteModels = new List<MovieModel>();
            foreach (Movie movie in moviesDb)
            {
                noteModels.Add(movie.ToMovieModel());
            }
            return noteModels;
        }

        public MovieModel GetMovieById(int id)
        {
            Movie noteDb = _movieRepository.GetById(id);
            if (noteDb == null)
            {
                throw new NotFoundException($"The Movie with id {id} was not found");
            }

            return noteDb.ToMovieModel();
        }

        public void UpdateMovie(MovieModel movieModel)
        {
            Movie movieDb = _movieRepository.GetById(movieModel.Id);
            if (movieDb == null)
            {
                throw new NotFoundException($"The Movie with id {movieModel.Id} was not found!");
            }
            User userDb = _userRepository.GetById(movieModel.UserId);
            if (userDb == null)
            {
                throw new NotFoundException($"The user with id {movieModel.UserId} was not found");
            }
            if (string.IsNullOrEmpty(movieModel.Title))
            {
                throw new MovieException("The property Title for Movie is required");
            }
            if (movieModel.Title.Length > 100)
            {
                throw new MovieException("The property Title can not contain more than 100 characters");
            }
            if (!string.IsNullOrEmpty(movieModel.Year.ToString()) && movieModel.Year.ToString().Length > 10)
            {
                throw new MovieException("The property Color can not contain more than 30 characters");
            }

            //movieDb.Id = movieModel.Id;
            //movieDb.Title = movieModel.Title;
            //movieDb.Description = movieModel.Description;
            //movieDb.Year = movieModel.Year;
            //movieDb.Genre = movieModel.Genre;
            //movieDb.UserId = movieModel.UserId;
            //movieDb.User = userDb;

            _movieRepository.Update(MovieMapper.MapMovie(movieDb,movieModel,userDb));

        }
    }
}
