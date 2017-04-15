using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MovieStore.DTOs.InputModels
{
    public class EditMovieBindingModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1900, 9999)]
        public int Year { get; set; }

        [Required]
        [Range(1, 300)]
        public int DurationInMinutes { get; set; }

        [Required]
        [Range(0.01, 100)]
        public double Size { get; set; }

        [Required]
        public string Poster { get; set; }

        [Required]
        public string Trailer { get; set; }

        [Required]
        [Range(0.01, 1000000)]
        public decimal Price { get; set; }

        [Required]
        [MinLength(20, ErrorMessage = "The description must be at least 20 symbols")]
        public string Description { get; set; }

        [Required]
        public string Country { get; set; }

        public List<string> ActorIds { get; set; }

        public MultiSelectList Actors { get; set; }

        public List<string> GenreIds { get; set; }

        public MultiSelectList Genres { get; set; }
    }
}
