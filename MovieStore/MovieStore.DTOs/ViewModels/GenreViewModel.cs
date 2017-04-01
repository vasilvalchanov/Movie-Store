using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MovieStore.Models.Models;

namespace MovieStore.DTOs.ViewModels
{
    public class GenreViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static Expression<Func<Genre, GenreViewModel>> Create
        {
            get
            {
                return g => new GenreViewModel()
                {
                    Id = g.Id,
                    Name = g.Name
                };
            }
        } 
    }
}
