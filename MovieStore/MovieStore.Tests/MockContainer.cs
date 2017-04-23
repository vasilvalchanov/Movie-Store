using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Moq;
using MovieStore.Data.Contracts;
using MovieStore.Models.Models;

namespace MovieStore.Tests
{
    public class MockContainer
    {
        public Mock<IRepository<Comment>> CommentRepositoryMock { get; set; }

        public Mock<IRepository<Actor>> ActorRepositoryMock { get; set; }

        public Mock<IRepository<Movie>> MovieRepositoryMock { get; set; }

        public Mock<IRepository<Rating>> RatingRepositoryMock { get; set; }

        public Mock<IRepository<User>> UserRepositoryMock { get; set; }

        public Mock<IRepository<Genre>> GenreRepositoryMock { get; set; }


        public void PrepareMocks()
        {
            this.SetupFakeGenres();
            this.SetupFakeUsers();    
            this.SetupFakeActors();
            this.SetupFakeMovies();
            this.SetupFakeRatings();
            this.SetupFakeComments();

        }

        private void SetupFakeGenres()
        {
            var fakeGenres = new List<Genre>()
            {
                new Genre() { Id = 1, Name = "Action"},
                new Genre() { Id = 2, Name = "Adventure"},
                new Genre() { Id = 3, Name = "Comedy"},
            };

            this.GenreRepositoryMock = new Mock<IRepository<Genre>>();
            this.GenreRepositoryMock.Setup(r => r.All()).Returns(fakeGenres.AsQueryable());
            this.GenreRepositoryMock.Setup(r => r.Find(It.IsAny<int>())).Returns(
                (int id) =>
                {
                    var genre = fakeGenres.FirstOrDefault(g => g.Id == id);
                    return genre;
                });
        }

        private void SetupFakeUsers()
        {
            var fakeRoles = new Dictionary<string, string>();
            fakeRoles.Add("1", "Administrator");
            fakeRoles.Add("2", "User");

            var fakeUsers = new List<User>()
            {
               new User() { Id = "123", UserName = "stamat" },
               new User() { Id = "1234", UserName = "temelko" },
               new User() { Id = "12345", UserName = "admin" },
            };

            var role = new IdentityUserRole();
            role.RoleId = fakeRoles.Keys.ElementAt(0);
            role.UserId = fakeUsers[2].Id;
            fakeUsers[2].Roles.Add(role);
            
            

            this.UserRepositoryMock = new Mock<IRepository<User>>();
            this.UserRepositoryMock.Setup(r => r.All()).Returns(fakeUsers.AsQueryable());
            this.UserRepositoryMock.Setup(r => r.Find(It.IsAny<int>())).Returns(
                (string id) =>
                {
                    var user = fakeUsers.FirstOrDefault(u => u.Id == id);
                    return user;
                });
        }

        private void SetupFakeRatings()
        {
            var fakeUsers = this.UserRepositoryMock.Object.All().ToList();
            var fakeMovies = this.MovieRepositoryMock.Object.All().ToList();

            var fakeRatings = new List<Rating>()
            {
               new Rating() {Id = 1, Stars = 7, Movie = fakeMovies[0], User = fakeUsers[0]},
               new Rating() {Id = 2, Stars = 5, Movie = fakeMovies[0], User = fakeUsers[0]},
               new Rating() {Id = 3, Stars = 9, Movie = fakeMovies[1], User = fakeUsers[1]}
            };

            this.RatingRepositoryMock = new Mock<IRepository<Rating>>();
            this.RatingRepositoryMock.Setup(r => r.All()).Returns(fakeRatings.AsQueryable());
            this.RatingRepositoryMock.Setup(r => r.Find(It.IsAny<int>())).Returns(
                (int id) =>
                {
                    var rating = fakeRatings.FirstOrDefault(r => r.Id == id);
                    return rating;
                });
        }

        private void SetupFakeMovies()
        {
            var fakeGenres = this.GenreRepositoryMock.Object.All().ToList();

            var fakeMovies = new List<Movie>()
            {
                new Movie()
                {
                    Id = 1,
                    Name = "Test Movie 1",
                    Country = "Test Country 1",
                    Description = "Test Test Test Test Test Test Test",
                    Price = 9.99m,
                    DurationInMinutes = 120,
                    Poster = "Test Poster",
                    Trailer = "Test Trailer",
                    Genres = fakeGenres
                },
                new Movie()
                {
                    Id = 2,
                    Name = "Test Movie 2",
                    Country = "Test Country 2",
                    Description = "Test Test Test Test Test Test Test",
                    Price = 9.99m,
                    DurationInMinutes = 120,
                    Poster = "Test Poster",
                    Trailer = "Test Trailer",
                    Genres = fakeGenres
                },
                new Movie()
                {
                    Id = 1,
                    Name = "Test Movie 1",
                    Country = "Test Country 1",
                    Description = "Test Test Test Test Test Test Test",
                    Price = 9.99m,
                    DurationInMinutes = 120,
                    Poster = "Test Poster",
                    Trailer = "Test Trailer",
                    Genres = fakeGenres
                }

            };

            this.MovieRepositoryMock = new Mock<IRepository<Movie>>();
            this.MovieRepositoryMock.Setup(r => r.All()).Returns(fakeMovies.AsQueryable());
            this.MovieRepositoryMock.Setup(r => r.Find(It.IsAny<int>())).Returns(
                (int id) =>
                {
                    var movie = fakeMovies.FirstOrDefault(m => m.Id == id);
                    return movie;
                });
        }

        private void SetupFakeActors()
        {

            var fakeActors = new List<Actor>()
            {
               new Actor() {Id = 1, IMDBProfile = "Test Profile 1", Name = "Test Name 1", Photo = "Test Photo 1", Movies = new List<Movie>()},
               new Actor() {Id = 2, IMDBProfile = "Test Profile 2", Name = "Test Name 2", Photo = "Test Photo 2", Movies = new List<Movie>()},
               new Actor() {Id = 3, IMDBProfile = "Test Profile 3", Name = "Test Name 3", Photo = "Test Photo 3", Movies = new List<Movie>()}
            };


            this.ActorRepositoryMock = new Mock<IRepository<Actor>>();
            this.ActorRepositoryMock.Setup(r => r.All()).Returns(fakeActors.AsQueryable());
            this.ActorRepositoryMock.Setup(r => r.Find(It.IsAny<int>())).Returns(
                (int id) =>
                {
                    var actor = fakeActors.FirstOrDefault(a => a.Id == id);
                    return actor;
                });
        }

        private void SetupFakeComments()
        {
            var fakeUsers = this.UserRepositoryMock.Object.All().ToList();
            var fakeMovies = this.MovieRepositoryMock.Object.All().ToList();

            var fakeComments = new List<Comment>()
            {
               new Comment() { Id = 1, CreatedAt = new DateTime(2017, 04, 23), Content = "Comment 1", Author = fakeUsers[0], Movie = fakeMovies[0]},
               new Comment() { Id = 2, CreatedAt = new DateTime(2017, 04, 24), Content = "Comment 2", Author = fakeUsers[0], Movie = fakeMovies[1]},
               new Comment() { Id = 3, CreatedAt = new DateTime(2017, 04, 23), Content = "Comment 3", Author = fakeUsers[1], Movie = fakeMovies[0]}
            };

            this.CommentRepositoryMock = new Mock<IRepository<Comment>>();
            this.CommentRepositoryMock.Setup(r => r.All()).Returns(fakeComments.AsQueryable());
            this.CommentRepositoryMock.Setup(r => r.Find(It.IsAny<int>())).Returns(
                (int id) =>
                {
                    var comment = fakeComments.FirstOrDefault(c => c.Id == id);
                    return comment;
                });
        }
    }
}
