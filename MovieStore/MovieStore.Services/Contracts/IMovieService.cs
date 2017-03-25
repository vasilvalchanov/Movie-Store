using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.DTOs.InputModels;
using MovieStore.DTOs.ViewModels;

namespace MovieStore.Services.Contracts
{
    public interface IMovieService
    {
        List<MovieViewModel> GetAllMovies();

        List<MovieViewModel> GetMoviesByUserId(int userId);

        void CreateMovie(CreateMovieBindingModel model);

        void EditMovie(EditMovieBindingModel model);

        MovieViewModel GetMovieViewById(int id);

        void Delete(int id);
    }
}
