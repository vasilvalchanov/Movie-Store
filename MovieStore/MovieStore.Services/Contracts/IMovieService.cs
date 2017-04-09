using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.DTOs.InputModels;
using MovieStore.DTOs.ViewModels;
using MovieStore.Models.Models;

namespace MovieStore.Services.Contracts
{
    public interface IMovieService
    {
        List<MovieViewModel> GetAllMovies();

        List<MovieViewModel> GetMoviesByUserId(string userId);

        IQueryable<GenreViewModel> GetAllMovieGenres(); 

        void CreateMovie(CreateMovieBindingModel model);

        void EditMovie(EditMovieBindingModel model);

        MovieViewModel GetMovieViewById(int id);

        void Delete(int id);

        CreateMovieBindingModel LoadCreateMovieData();

        MovieViewModel LoadEditMovieData(int id);

        void RateMovie(int id, RateMovieInputModel model, string currentUserId);

        bool HasBeenMovieAlreadyRated(int movieId, string currentUserId);

        void BuyMovie(int id, string currentUserId);

        bool HasBeenMovieAlreadyBought(int id, string currentUserId);
    }
}
