using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.Data.Contracts;

namespace MovieStore.Data.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        public GenericRepository(DbContext context)
        {
            this.Context = context;
            this.Set = context.Set<T>();
        }

        protected IDbSet<T> Set { get; set; }

        protected DbContext Context { get; set; }

        public IQueryable<T> All()
        {
            return this.Set.AsQueryable();
        }

        public T Find(object id)
        {
            return this.Set.Find(id);
        }

        public void Add(T entity)
        {
            this.ChangeState(entity, EntityState.Added);
        }

        public void Delete(T entity)
        {
            this.ChangeState(entity, EntityState.Deleted);
        }

        public void Update(T entity)
        {
            this.ChangeState(entity, EntityState.Modified);
        }

        private void ChangeState(T entity, EntityState state)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.Set.Attach(entity);
            }

            entry.State = state;
        }
    }
}
