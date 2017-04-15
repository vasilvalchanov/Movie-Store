using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MovieStore.Models.Models;

namespace MovieStore.DTOs.ViewModels
{
    public class MovieViewModel : SimpleMovieViewModel
    {
        public int Year { get; set; }

        public int DurationInMinutes { get; set; }

        public double Size { get; set; }

        public string Poster { get; set; }

        public string Trailer { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Country { get; set; }

        public double? Rating { get; set; }

        public int Comments { get; set; }

        public IEnumerable<GenreViewModel> Genres { get; set; }

        public IEnumerable<ActorSimpleViewModel> Actors { get; set; }

        public List<string> ActorIds { get; set; }

        public MultiSelectList ActorsSelectList { get; set; }

        public List<string> GenreIds { get; set; }

        public MultiSelectList GenresSelectList { get; set; }

        public static Expression<Func<Movie, MovieViewModel>> Create
        {
            get
            {
                return m => new MovieViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Year = m.Year,
                    DurationInMinutes = m.DurationInMinutes,
                    Size = m.Size,
                    Poster = m.Poster,
                    Trailer = m.Trailer,
                    Price = m.Price,
                    Description = m.Description,
                    Country = m.Country,
                    Comments = m.Comments.Count,
                    Rating = m.Ratings.Average(rt => rt.Stars),
                    Genres = m.Genres.Select(g => new GenreViewModel() {Id = g.Id, Name = g.Name}),
                    Actors = m.Actors.Select(a => new ActorSimpleViewModel() {Id = a.Id, Name = a.Name})
                };
            }
        }

    }
}
