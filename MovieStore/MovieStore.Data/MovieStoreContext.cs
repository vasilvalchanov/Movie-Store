using Microsoft.AspNet.Identity.EntityFramework;
using MovieStore.Data.Contracts;
using MovieStore.Data.Migrations;
using MovieStore.Models.Models;

namespace MovieStore.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MovieStoreContext : IdentityDbContext<User>, IMovieStoreContext
    {
        public MovieStoreContext()
            : base("name=MovieStoreContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MovieStoreContext, Configuration>());
        }

        public virtual DbSet<Actor> Actors { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<Movie> Movies { get; set; }

        public virtual DbSet<Rating> Ratings { get; set; }

        public virtual DbSet<Genre> Genres { get; set; }

        public static MovieStoreContext Create()
        {
            return new MovieStoreContext();
        }

        public DbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
    }

}