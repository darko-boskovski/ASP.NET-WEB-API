using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi_Workshop.Domain.Models;
using WebApi_Workshop_Class05.Models;

namespace WebApi_Workshop.DataAccess.Implementations
{
    public class MovieRepository : IRepository<Movie>
    {
        private MoviesAppDbContext _moviesAppDbContext;

        public MovieRepository(MoviesAppDbContext movieAppDbContext)
        {
            _moviesAppDbContext = movieAppDbContext;
        }




        public void Add(Movie entity)
        {
            _moviesAppDbContext.Movies.Add(entity);
            _moviesAppDbContext.SaveChanges();
        }

        public void Delete(Movie entity)
        {
            _moviesAppDbContext.Movies.Remove(entity);
            _moviesAppDbContext.SaveChanges();
        }

        public List<Movie> GetAll()
        {
            return _moviesAppDbContext.Movies
                .Include(x => x.User)
                .ToList();

        }

        public Movie GetById(int id)
        {
            return _moviesAppDbContext
                .Movies
                .Include(x => x.User)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(Movie entity)
        {
            _moviesAppDbContext.Movies.Update(entity);
            _moviesAppDbContext.SaveChanges();
        }
    }
}
