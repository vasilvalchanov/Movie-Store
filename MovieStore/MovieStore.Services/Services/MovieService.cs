using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.Data.Contracts;
using MovieStore.DTOs.InputModels;
using MovieStore.DTOs.ViewModels;
using MovieStore.Services.Contracts;

namespace MovieStore.Services.Services
{
    public class MovieService : BaseService, IMovieService
    {
        public MovieService(IMovieStoreData data) : base(data)
        {
        }

        public List<MovieViewModel> GetAllMovies()
        {
            var movies = this.Data.Movies.All()
                .Include(m => m.Actors)
                .Include(m => m.Ratings)
                .Include(m => m.Genres)
                .Select(MovieViewModel.Create).ToList();

            return movies;
        }

        public List<MovieViewModel> GetMoviesByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public void CreateMovie(CreateMovieBindingModel model)
        {
            throw new NotImplementedException();
        }

        public void EditMovie(EditMovieBindingModel model)
        {
            throw new NotImplementedException();
        }

        public MovieViewModel GetMovieViewById(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
