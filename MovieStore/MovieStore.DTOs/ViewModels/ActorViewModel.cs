using MovieStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DTOs.ViewModels
{
    public class ActorViewModel : ActorSimpleViewModel
    {
        public string Photo { get; set; }

        public string IMDBProfile { get; set; }

        public IEnumerable<SimpleMovieViewModel> Movies { get; set; }

        public static Expression<Func<Actor, ActorViewModel>> Create
        {
            get
            {
                return a => new ActorViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Photo = a.Photo,
                    IMDBProfile = a.IMDBProfile,
                    Movies = a.Movies.Select(m => new SimpleMovieViewModel() { Id = m.Id, Name = m.Name})
                };
            }
        }
    }
}
