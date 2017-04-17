using MovieStore.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MovieStore.DTOs.InputModels;
using MovieStore.DTOs.ViewModels;
using MovieStore.Models.Models;

namespace MovieStore.Services.Services
{
    public class BaseService
    {
        public BaseService(IMovieStoreData data)
        {
            this.Data = data;
        }

        public IMovieStoreData Data { get; set; }

        protected void CheckModelForNull(object model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "The input model cannot be null");
            }
        }

    }
}
