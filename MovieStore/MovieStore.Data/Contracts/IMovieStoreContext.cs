using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.Models.Models;

namespace MovieStore.Data.Contracts
{
    public interface IMovieStoreContext
    {
        DbSet<Actor> Actors { get; set; }

        DbSet<Comment> Comments { get; set; }

        DbSet<Movie> Movies { get; set; }

        DbSet<Rating> Ratings { get; set; }

        DbSet<Genre> Genres { get; set; }

        int SaveChanges();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        DbSet<T> Set<T>() where T : class;
    }
}
