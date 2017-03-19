using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MovieStore.Models.Models
{
    public class User : IdentityUser
    {
        private ICollection<Rating> ratings;
        private ICollection<Movie> movies;
        private ICollection<Comment> comments;

        public User()
        {
            this.ratings = new HashSet<Rating>();
            this.movies = new HashSet<Movie>();
            this.comments = new HashSet<Comment>();
        }

        [Required]
        public string Fullname { get; set; }

        public virtual ICollection<Movie> Movies
        {
            get { return this.movies; }
            set { this.movies = value; }
        }

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

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            return await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}
