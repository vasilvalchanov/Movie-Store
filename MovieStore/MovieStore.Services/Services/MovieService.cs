using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.Data.Contracts;
using MovieStore.DTOs.InputModels;
using MovieStore.DTOs.ViewModels;
using MovieStore.Models.Models;
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
            return null;
        }

        public MovieViewModel GetMovieViewById(int id)
        {
            var movie = this.Data.Movies.All()
                .Where(a => a.Id == id)
                .Select(MovieViewModel.Create)
                .FirstOrDefault();

            if (movie == null)
            {
                throw new ArgumentNullException("There is not such actor in database");
            }

            return movie;
        }

        public void CreateMovie(CreateMovieBindingModel model)
        {
            this.CheckModelForNull(model);
            this.CheckMovieForDuplication(model);
            var genresId = model.GenreIds.Select(int.Parse);
            var genres = this.Data.Genres.All().Where(g => genresId.Any(id => id == g.Id)).ToList();
            var actorsId = model.ActorIds.Select(int.Parse);
            var actors = this.Data.Actors.All().Where(a => actorsId.Any(id => id == a.Id)).ToList();


            var movie = new Movie()
            {
                Name = model.Name,
                Year = model.Year,
                DurationInMinutes = model.DurationInMinutes,
                Size = model.Size,
                Price = model.Price,
                Poster = model.Poster,
                Trailer = model.Trailer,
                Description = model.Description,
                Country = model.Country,
                Genres = genres,
                Actors = actors
            };

            this.Data.Movies.Add(movie);
            this.Data.SaveChanges();
        }

        public void EditMovie(EditMovieBindingModel model)
        {
            this.CheckModelForNull(model);
            this.CheckMovieForDuplication(model);

            var genresId = model.GenreIds.Select(int.Parse);
            var genres = this.Data.Genres.All().Where(g => genresId.Any(id => id == g.Id)).ToList();
            var actorsId = model.ActorIds.Select(int.Parse);
            var actors = this.Data.Actors.All().Where(a => actorsId.Any(id => id == a.Id)).ToList();

            var movie = this.GetMovieById(model.Id);
            movie.Name = model.Name;
            movie.Year = model.Year;
            movie.DurationInMinutes = model.DurationInMinutes;
            movie.Size = model.Size;
            movie.Price = model.Price;
            movie.Poster = model.Poster;
            movie.Trailer = model.Trailer;
            movie.Description = model.Description;
            movie.Country = model.Country;
            movie.Genres = genres;
            movie.Actors = actors;

            this.Data.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<GenreViewModel> GetAllMovieGenres()
        {
            var genres = this.Data.Genres.All()
                .Select(GenreViewModel.Create);

            return genres;
        }

        private Movie GetMovieById(int id)
        {
            var movie = this.Data.Movies
                .All()
                .FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                throw new ArgumentNullException("There is not such movie in database");
            }

            return movie;
        }

        private void CheckMovieForDuplication(CreateMovieBindingModel model)
        {
            var doesItExist = this.Data.Movies.All()
                .Any(m => m.Name == model.Name && m.Year == model.Year);

            if (doesItExist)
            {
                throw new InvalidOperationException($"There is already movie with name {model.Name} and year ${model.Year}");
            }
        }

        private void CheckMovieForDuplication(EditMovieBindingModel model)
        {
            var doesItExist = this.Data.Movies.All()
                .Any(m => m.Name == model.Name && m.Year == model.Year && m.Id != model.Id);

            if (doesItExist)
            {
                throw new InvalidOperationException($"There is already movie with name {model.Name} and year ${model.Year}");
            }
        }


    }
}
