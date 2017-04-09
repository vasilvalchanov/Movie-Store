using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.Data.Contracts;

namespace MovieStore.Services.Contracts
{
    public interface IBaseService
    {
        IMovieStoreData Data { get; set; }
    }
}
