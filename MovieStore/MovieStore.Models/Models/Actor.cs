using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Models.Models
{
    public class Actor
    {
        private ICollection<Movie> movies;

        public Actor()
        {
            this.movies = new HashSet<Movie>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string IMDBProfile { get; set; }

        public virtual ICollection<Movie> Movies
        {
            get { return this.movies; }
            set { this.movies = value; }
        }
    }
}
