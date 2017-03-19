using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data.Contracts
{
    public interface IRepository<T>
    {
        IQueryable<T> All();

        T Find(object id);

        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}
