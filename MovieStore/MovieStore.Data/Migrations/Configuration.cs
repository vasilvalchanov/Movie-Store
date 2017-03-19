using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MovieStore.Models.Enums;
using MovieStore.Models.Models;

namespace MovieStore.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MovieStoreContext>
    {
        private List<Actor> actors; 

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MovieStoreContext context)
        {
            if (!context.Users.Any())
            {
                var adminEmail = "admin@admin.com";
                var adminUsername = "admin";
                var adminFullname = "Administrator";
                var adminPassword = "admin";
                var adminRole = "Administrator";

                this.CreateAdminUser(context, adminEmail, adminUsername, adminFullname, adminPassword, adminRole);
            }

            if (!context.Actors.Any())
            {
                this.SeedActors(context);
            }

            if (!context.Movies.Any())
            {
                this.SeedMovies(context);
            }
        }

        private void SeedActors(MovieStoreContext context)
        {
            this.actors = new List<Actor>()
            {
                new Actor() {Name = "Hugh Jackman", IMDBProfile = "http://www.imdb.com/name/nm0413168/"},
                new Actor() {Name = "Tom Hardy", IMDBProfile = "http://www.imdb.com/name/nm0362766/"},
                new Actor() {Name = "Jennifer Lawrence", IMDBProfile = "http://www.imdb.com/name/nm2225369/"},
                new Actor() {Name = "Marion Cotillard", IMDBProfile = "http://www.imdb.com/name/nm0182839/"},
                new Actor() {Name = "Leonardo DiCaprio", IMDBProfile = "http://www.imdb.com/name/nm0000138/"},
                new Actor() {Name = "Ryan Reynold", IMDBProfile = "http://www.imdb.com/name/nm0005351/"},
                new Actor() {Name = "Brad Pitt", IMDBProfile = "http://www.imdb.com/name/nm0000093/"},
                new Actor() {Name = "Jessica Chastain", IMDBProfile = "http://www.imdb.com/name/nm1567113/"},
                new Actor() {Name = "Ben Affleck", IMDBProfile = "http://www.imdb.com/name/nm0000255/"},
                new Actor() {Name = "Alexandra Daddario", IMDBProfile = "http://www.imdb.com/name/nm1275259/"},
                new Actor() {Name = "Dwayne Johnson", IMDBProfile = "http://www.imdb.com/name/nm0425005/?ref_=tt_ov_st_sm"},
                new Actor() {Name = "Vin Diesel", IMDBProfile = "http://www.imdb.com/name/nm0004874/?ref_=tt_ov_st_sm"},
                new Actor() {Name = "Matt Damon", IMDBProfile = "http://www.imdb.com/name/nm0000354/?ref_=tt_ov_st_sm"}
            };

            context.Actors.AddRange(actors);
            context.SaveChanges();
        }

        private void SeedMovies(MovieStoreContext context)
        {
            var movies = new List<Movie>()
            {
                new Movie()
                {
                    Name = "Allied",
                    Year = 2016,
                    DurationInMinutes = 124,
                    Size = 5.46,
                    Poster = "https://images-na.ssl-images-amazon.com/images/M/MV5BMjA0MTkzMDI1MF5BMl5BanBnXkFtZTgwMjQxNDE0MDI@._V1_SY1000_CR0,0,640,1000_AL_.jpg",
                    Trailer = "http://www.imdb.com/title/tt3640424/videoplayer/vi4001674777",
                    Price = 15.99m,
                    Description = "In 1942, a Canadian intelligence officer in North Africa encounters a female French Resistance fighter on a deadly mission behind enemy lines. When they reunite in London, their relationship is tested by the pressures of war.",
                    Country = "UK, USA",
                    Genres = new HashSet<Genre>() {Genre.Action, Genre.Drama, Genre.Romance },
                    Actors = new HashSet<Actor>() {this.actors[3], this.actors[6] }
                },
                new Movie()
                {
                    Name = "Inception",
                    Year = 2010,
                    DurationInMinutes = 148,
                    Size = 1.47,
                    Poster = "https://images-na.ssl-images-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_SY1000_CR0,0,675,1000_AL_.jpg",
                    Trailer = "http://www.imdb.com/title/tt1375666/videoplayer/vi4219471385",
                    Price = 9.99m,
                    Description = "A thief, who steals corporate secrets through use of dream-sharing technology, is given the inverse task of planting an idea into the mind of a CEO.",
                    Country = "UK, USA",
                    Genres = new HashSet<Genre>() {Genre.Action, Genre.Adventure, Genre.Thriller, Genre.Mystery },
                    Actors = new HashSet<Actor>() {this.actors[3], this.actors[1], this.actors[4] }
                },
                new Movie()
                {
                    Name = "The Dark Knight Rises",
                    Year = 2012,
                    DurationInMinutes = 164,
                    Size = 2.26,
                    Poster = "https://images-na.ssl-images-amazon.com/images/M/MV5BMTk4ODQzNDY3Ml5BMl5BanBnXkFtZTcwODA0NTM4Nw@@._V1_.jpg",
                    Trailer = "http://www.imdb.com/title/tt1345836/videoplayer/vi2376312089",
                    Price = 1m,
                    Description = "Eight years after the Joker's reign of anarchy, the Dark Knight, with the help of the enigmatic Selina, is forced from his imposed exile to save Gotham City, now on the edge of total annihilation, from the brutal guerrilla terrorist Bane.",
                    Country = "UK, USA",
                    Genres = new HashSet<Genre>() { Genre.Action, Genre.Thriller},
                    Actors = new HashSet<Actor>() {this.actors[3], this.actors[1]}
                },
                new Movie()
                {
                    Name = "Ocean's Twelve",
                    Year = 2004,
                    DurationInMinutes = 125,
                    Size = 4.36,
                    Poster = "https://images-na.ssl-images-amazon.com/images/M/MV5BMTQ3NTIyMzczMF5BMl5BanBnXkFtZTYwMjgzNTg2._V1_.jpg",
                    Trailer = "https://www.youtube.com/watch?v=bkYCR677_OQ",
                    Price = 11.99m,
                    Description = "Daniel Ocean recruits one more team member so he can pull off three major European heists in this sequel to Ocean's 11.",
                    Country = "USA",
                    Genres = new HashSet<Genre>() {Genre.Crime, Genre.Thriller },
                    Actors = new HashSet<Actor>() {this.actors[6], this.actors[12]}
                },
                new Movie()
                {
                    Name = "X-Men Origins: Wolverine",
                    Year = 2009,
                    DurationInMinutes = 103,
                    Size = 4.1,
                    Poster = "https://images-na.ssl-images-amazon.com/images/M/MV5BZWRhMzdhMzEtZTViNy00YWYyLTgxZmUtMTMwMWM0NTEyMjk3XkEyXkFqcGdeQXVyNTIzOTk5ODM@._V1_SY1000_CR0,0,676,1000_AL_.jpg",
                    Trailer = "http://www.imdb.com/videoplayer/vi3012297497",
                    Price = 13.99m,
                    Description = "A look at Wolverine's early life, in particular his time with the government squad Team X and the impact it will have on his later years.",
                    Country = "UK, USA",
                    Genres = new HashSet<Genre>() { Genre.Action, Genre.Adventure, Genre.Thriller, Genre.Fantasy},
                    Actors = new HashSet<Actor>() {this.actors[0], this.actors[5]}
                },
                new Movie()
                {
                    Name = "Deadpool",
                    Year = 2016,
                    DurationInMinutes = 108,
                    Size = 4.43,
                    Poster = "https://images-na.ssl-images-amazon.com/images/M/MV5BMjQyODg5Njc4N15BMl5BanBnXkFtZTgwMzExMjE3NzE@._V1_SY1000_SX686_AL_.jpg",
                    Trailer = "http://www.imdb.com/title/tt1431045/videoplayer/vi567457049",
                    Price = 19.99m,
                    Description = "A fast-talking mercenary with a morbid sense of humor is subjected to a rogue experiment that leaves him with accelerated healing powers and a quest for revenge.",
                    Country = "USA",
                    Genres = new HashSet<Genre>() { Genre.Action, Genre.Adventure, Genre.Comedy},
                    Actors = new HashSet<Actor>() {this.actors[5]}
                },
                new Movie()
                {
                    Name = "The Departed",
                    Year = 2006,
                    DurationInMinutes = 151,
                    Size = 1.87,
                    Poster = "https://images-na.ssl-images-amazon.com/images/M/MV5BMTI1MTY2OTIxNV5BMl5BanBnXkFtZTYwNjQ4NjY3._V1_.jpg",
                    Trailer = "https://www.youtube.com/watch?v=auYbpnEwBBg",
                    Price = 14.99m,
                    Description = "An undercover cop and a mole in the police attempt to identify each other while infiltrating an Irish gang in South Boston.",
                    Country = "USA, Hong Kong",
                    Genres = new HashSet<Genre>() { Genre.Crime, Genre.Drama, Genre.Thriller},
                    Actors = new HashSet<Actor>() {this.actors[4], this.actors[12]}
                },
                new Movie()
                {
                    Name = "San Andreas",
                    Year = 2015,
                    DurationInMinutes = 114,
                    Size = 4.93,
                    Poster = "https://images-na.ssl-images-amazon.com/images/M/MV5BNjI4MTgyOTAxOV5BMl5BanBnXkFtZTgwMjQwOTA4NTE@._V1_SY1000_SX675_AL_.jpg",
                    Trailer = "http://www.imdb.com/title/tt2126355/videoplayer/vi395358233",
                    Price = 9.99m,
                    Description = "In the aftermath of a massive earthquake in California, a rescue-chopper pilot makes a dangerous journey with his ex-wife across the state in order to rescue his daughter.",
                    Country = "USA",
                    Genres = new HashSet<Genre>() { Genre.Action, Genre.Adventure, Genre.Drama, Genre.Thriller},
                    Actors = new HashSet<Actor>() {this.actors[9], this.actors[10]}
                },
                new Movie()
                {
                    Name = "Furious 7",
                    Year = 2015,
                    DurationInMinutes = 140,
                    Size = 1.46,
                    Poster = "https://images-na.ssl-images-amazon.com/images/M/MV5BMTQxOTA2NDUzOV5BMl5BanBnXkFtZTgwNzY2MTMxMzE@._V1_SY1000_CR0,0,631,1000_AL_.jpg",
                    Trailer = "http://www.imdb.com/title/tt2820852/videoplayer/vi1346743833",
                    Price = 14.99m,
                    Description = "Deckard Shaw seeks revenge against Dominic Toretto and his family for his comatose brother.",
                    Country = "USA, Japan",
                    Genres = new HashSet<Genre>() { Genre.Action, Genre.Crime, Genre.Thriller},
                    Actors = new HashSet<Actor>() {this.actors[11], this.actors[10]}
                },
                new Movie()
                {
                    Name = "The Martian",
                    Year = 2015,
                    DurationInMinutes = 142,
                    Size = 4.43,
                    Poster = "https://images-na.ssl-images-amazon.com/images/M/MV5BMTc2MTQ3MDA1Nl5BMl5BanBnXkFtZTgwODA3OTI4NjE@._V1_SY1000_CR0,0,675,1000_AL_.jpg",
                    Trailer = "http://www.imdb.com/title/tt3659388/videoplayer/vi113423129",
                    Price = 19.99m,
                    Description = "An astronaut becomes stranded on Mars after his team assume him dead, and must rely on his ingenuity to find a way to signal to Earth that he is alive.",
                    Country = "UK, USA",
                    Genres = new HashSet<Genre>() { Genre.Adventure, Genre.Drama, Genre.Fantasy},
                    Actors = new HashSet<Actor>() {this.actors[12], this.actors[7]}
                },
                new Movie()
                {
                    Name = "Good Will Hunting",
                    Year = 1997,
                    DurationInMinutes = 126,
                    Size = 1.38,
                    Poster = "https://images-na.ssl-images-amazon.com/images/M/MV5BOTI0MzcxMTYtZDVkMy00NjY1LTgyMTYtZmUxN2M3NmQ2NWJhXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_SY1000_CR0,0,655,1000_AL_.jpg",
                    Trailer = "https://www.youtube.com/watch?v=QSMvyuEeIyw",
                    Price = 9.99m,
                    Description = "Will Hunting, a janitor at M.I.T., has a gift for mathematics, but needs help from a psychologist to find direction in his life",
                    Country = "USA",
                    Genres = new HashSet<Genre>() { Genre.Drama},
                    Actors = new HashSet<Actor>() {this.actors[8], this.actors[12]}
                    // matt damon, ben aflek
                },
                 new Movie()
                {
                    Name = "X-Men: Days of Future Past",
                    Year = 2014,
                    DurationInMinutes = 131,
                    Size = 2.17,
                    Poster = "https://images-na.ssl-images-amazon.com/images/M/MV5BZGIzNWYzN2YtMjcwYS00YjQ3LWI2NjMtOTNiYTUyYjE2MGNkXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_SY1000_CR0,0,675,1000_AL_.jpg",
                    Trailer = "http://www.imdb.com/title/tt1877832/videoplayer/vi3858345241",
                    Price = 12.99m,
                    Description = "The X-Men send Wolverine to the past in a desperate effort to change history and prevent an event that results in doom for both humans and mutants.",
                    Country = "USA, UK, Canada",
                    Genres = new HashSet<Genre>() { Genre.Action, Genre.Adventure, Genre.Thriller, Genre.Fantasy},
                    Actors = new HashSet<Actor>() {this.actors[2], this.actors[10]}
                }
            };

            context.Movies.AddRange(movies);
            context.SaveChanges();
        }

        private void CreateAdminUser(MovieStoreContext context, string adminEmail, string adminUsername, string adminFullname, string adminPassword, string adminRole)
        {
            //create the "admin" user
            var adminUser = new User()
            {
                UserName = adminUsername,
                Fullname = adminFullname,
                Email = adminEmail
            };

            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);

            userManager.PasswordValidator = new PasswordValidator()
            {

                RequiredLength = 5,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            var userCreateResult = userManager.Create(adminUser, adminPassword);

            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }

            //create the "Administrator" role


            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var roleCreateResult = roleManager.Create(new IdentityRole(adminRole));

            if (!roleCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", roleCreateResult.Errors));
            }

            // add the admin user to the Administrator role

            var addAdminRoleResult = userManager.AddToRole(adminUser.Id, adminRole);

            if (!addAdminRoleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
            }
        }
    }
}
