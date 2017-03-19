using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.Data.Contracts;
using MovieStore.Data.Repositories;
using MovieStore.Models.Models;

namespace MovieStore.Data
{
    public class MovieStoreData : IMovieStoreData
    {
        private readonly DbContext context;

        private readonly IDictionary<Type, object> repositories;

        public MovieStoreData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<Actor> Actors => this.GetRepository<Actor>();

        public IRepository<Comment> Comments => this.GetRepository<Comment>();

        public IRepository<Movie> Movies => this.GetRepository<Movie>();

        public IRepository<Rating> Ratings => this.GetRepository<Rating>();

        public IRepository<User> Users => this.GetRepository<User>();

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);
            if (!this.repositories.ContainsKey(type))
            {
                var typeOfRepository = typeof(GenericRepository<T>);
                var repository = Activator.CreateInstance(
                    typeOfRepository, this.context);

                this.repositories.Add(type, repository);
            }

            return (IRepository<T>)this.repositories[type];
        }
    }
}
