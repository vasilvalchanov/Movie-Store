using MovieStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data.Contracts
{
    public interface IMovieStoreData
    {
        IRepository<Actor> Actors { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Movie> Movies { get; }

        IRepository<Rating> Ratings { get; }

        IRepository<User> Users { get; }

        IRepository<Genre> Genres { get; }

        int SaveChanges();
    }
}
