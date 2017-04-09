using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MovieStore.Data.Contracts;
using MovieStore.DTOs.InputModels;
using MovieStore.DTOs.ViewModels;
using MovieStore.Models.Models;
using MovieStore.Services.Contracts;

namespace MovieStore.Services.Services
{
    public class MovieService : BaseService, IMovieService
    {
        private readonly IActorsService actorsService;

        public MovieService(IMovieStoreData data, IActorsService actorsService) : base(data)
        {
            this.actorsService = actorsService;
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

        public List<MovieViewModel> GetMoviesByUserId(string userId)
        {
            var movies = this.Data.Movies.All()
                .Include(m => m.Actors)
                .Include(m => m.Ratings)
                .Include(m => m.Genres)
                .Where(m => m.Users.Any(u => u.Id == userId))
                .Select(MovieViewModel.Create).ToList();

            return movies;
        }

        public CreateMovieBindingModel LoadCreateMovieData()
        {
            var model = new CreateMovieBindingModel();
            var actors = this.actorsService.GetAllActors().ToList();
            var genres = this.GetAllMovieGenres().ToList();
            var actorItems = actors.Select(actor => new SelectListItem()
            {
                Value = actor.Id.ToString(),
                Text = actor.Name
            }).ToList();

            var genreItems = genres.Select(genre => new SelectListItem()
            {
                Value = genre.Id.ToString(),
                Text = genre.Name
            }).ToList();

            MultiSelectList actorsList = new MultiSelectList(actorItems.OrderBy(i => i.Text), "Value", "Text");
            MultiSelectList genreList = new MultiSelectList(genreItems.OrderBy(i => i.Text), "Value", "Text");

            model.Actors = actorsList;
            model.Genres = genreList;

            return model;
        }

        public MovieViewModel LoadEditMovieData(int id)
        {
            var movie = this.GetMovieViewById(id);
            //var model = new CreateMovieBindingModel();
            var actors = this.actorsService.GetAllActors().ToList();
            var genres = this.GetAllMovieGenres().ToList();
            var selectedActorsIds = new List<string>();
            var selectedGenresIds = new List<string>();
            selectedActorsIds.AddRange(movie.Actors.Select(a => a.Id.ToString()));
            selectedGenresIds.AddRange(movie.Genres.Select(a => a.Id.ToString()));
            var actorItems = actors.Select(actor => new SelectListItem()
            {
                Value = actor.Id.ToString(),
                Text = actor.Name
            }).ToList();

            var genreItems = genres.Select(genre => new SelectListItem()
            {
                Value = genre.Id.ToString(),
                Text = genre.Name
            }).ToList();

            MultiSelectList actorsList = new MultiSelectList(actorItems.OrderBy(i => i.Text), "Value", "Text");
            MultiSelectList genreList = new MultiSelectList(genreItems.OrderBy(i => i.Text), "Value", "Text");

            movie.ActorsSelectList = new MultiSelectList(actorsList, "Value", "Text", selectedActorsIds);
            movie.GenresSelectList = new MultiSelectList(genreList, "Value", "Text", selectedGenresIds);

            return movie;
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

            var genresToDelete = new List<Genre>();

            if (genresId.Count() < movie.Genres.Count)
            {
                genresToDelete.AddRange(movie.Genres.Where(genre => !genresId.Any(id => id == genre.Id)));
            }
            else
            {
                movie.Genres = genres;
            }

            var actorsToDelete = new List<Actor>();

            if (actorsId.Count() < movie.Actors.Count)
            {
                actorsToDelete.AddRange(movie.Actors.Where(actor => !actorsId.Any(id => id == actor.Id)));
            }
            else
            {
                movie.Actors = actors;
            }

            foreach (var act in actorsToDelete)
            {
                movie.Actors.Remove(act);
            }

            foreach (var genre in genresToDelete)
            {
                movie.Genres.Remove(genre);
            }

            this.Data.SaveChanges();
        }

        public void Delete(int id)
        {
            var movie = this.GetMovieById(id);
            this.Data.Movies.Delete(movie);
            this.Data.SaveChanges();
        }

        public IQueryable<GenreViewModel> GetAllMovieGenres()
        {
            var genres = this.Data.Genres.All()
                .Select(GenreViewModel.Create);

            return genres;
        }

        public bool HasBeenMovieAlreadyRated(int movieId, string currentUserId)
        {
            var movie = this.GetMovieById(movieId);
            var hasBeenRated = movie.Ratings.FirstOrDefault(r => r.UserId == currentUserId) != null;
            return hasBeenRated;
        }

        public void RateMovie(int id, RateMovieInputModel model, string currentUserId)
        {
            this.CheckModelForNull(model);

            var movie = this.GetMovieById(id);

            var rating = new Rating
            {
                MovieId = movie.Id,
                UserId = currentUserId,
                Stars = model.Stars
            };

            this.Data.Ratings.Add(rating);

            this.Data.SaveChanges();
        }

        public bool HasBeenMovieAlreadyBought(int id, string currentUserId)
        {
            var movie = this.GetMovieById(id);
            var hasBeenRated = movie.Users.FirstOrDefault(m => m.Id == currentUserId) != null;
            return hasBeenRated;
        }

        public void BuyMovie(int id, string currentUserId)
        {
            var movie = this.GetMovieById(id);
            var user = this.Data.Users.All()
                .FirstOrDefault(u => u.Id == currentUserId);

            movie.Users.Add(user);
            this.Data.SaveChanges();
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
