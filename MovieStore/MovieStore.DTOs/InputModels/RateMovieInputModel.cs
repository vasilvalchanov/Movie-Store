using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DTOs.InputModels
{
    public class RateMovieInputModel
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 10)]
        public int Stars { get; set; }
    }
}
