using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.Models.Enums;

namespace MovieStore.Models.Models
{
    public class Movie
    {
        private ICollection<Rating> ratings;
        private ICollection<User> users;
        private ICollection<Actor> actors;
        private ICollection<Comment> comments;

        public Movie()
        {
            this.ratings = new HashSet<Rating>();
            this.users = new HashSet<User>();
            this.actors = new HashSet<Actor>();
            this.comments = new HashSet<Comment>();
            this.Genres = new HashSet<Genre>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int DurationInMinutes { get; set; }

        [Required]
        public double Size { get; set; }

        [Required]
        public string Poster { get; set; }

        public string Trailer { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Country { get; set; }

        public ICollection<Genre> Genres { get; set; }

        public virtual ICollection<Rating> Ratings
        {
            get { return this.ratings; }
            set { this.ratings = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual ICollection<Actor> Actors
        {
            get { return this.actors; }
            set { this.actors = value; }
        }

        public virtual ICollection<User> Users
        {
            get { return this.users; }
            set { this.users = value; }
        }

    }
}
