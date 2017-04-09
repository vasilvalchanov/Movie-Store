using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
        private List<Genre> genres;

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

            if (!context.Genres.Any())
            {
                this.SeedGenres(context);
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

        private void SeedGenres(MovieStoreContext context)
        {
            this.genres = new List<Genre>()
            {
                new Genre() { Name = "Action"},
                new Genre() { Name = "Adventure"},
                new Genre() { Name = "Comedy"},
                new Genre() { Name = "Crime"},
                new Genre() { Name = "Drama"},
                new Genre() { Name = "Fantasy"},
                new Genre() { Name = "Horror"},
                new Genre() { Name = "Mystery"},
                new Genre() { Name = "Romance"},
                new Genre() { Name = "Thriller"}
            };

            context.Genres.AddRange(genres);
            context.SaveChanges();
        }

        private void SeedActors(MovieStoreContext context)
        {
            this.actors = new List<Actor>()
            {
                new Actor() {Name = "Hugh Jackman", Photo = "http://vignette3.wikia.nocookie.net/marvelmovies/images/3/31/Hugh_Jackman.jpg/revision/latest?cb=20130821201217", IMDBProfile = "http://www.imdb.com/name/nm0413168/"},
                new Actor() {Name = "Tom Hardy", Photo = "http://static.celebuzz.com/uploads/2011/08/30/Tom-Hardy-5.jpg", IMDBProfile = "http://www.imdb.com/name/nm0362766/"},
                new Actor() {Name = "Jennifer Lawrence", Photo = "https://s-media-cache-ak0.pinimg.com/originals/2f/2f/ba/2f2fba3617e89c30aa15dd19635b3bba.jpg", IMDBProfile = "http://www.imdb.com/name/nm2225369/"},
                new Actor() {Name = "Marion Cotillard", Photo = "https://images-na.ssl-images-amazon.com/images/M/MV5BMTQxNTEzNTkwNF5BMl5BanBnXkFtZTcwNzQ2NDIwOQ@@._V1_UX214_CR0,0,214,317_AL_.jpg", IMDBProfile = "http://www.imdb.com/name/nm0182839/"},
                new Actor() {Name = "Leonardo DiCaprio", Photo = "https://images-na.ssl-images-amazon.com/images/M/MV5BMjI0MTg3MzI0M15BMl5BanBnXkFtZTcwMzQyODU2Mw@@._V1_UY1200_CR130,0,630,1200_AL_.jpg", IMDBProfile = "http://www.imdb.com/name/nm0000138/"},
                new Actor() {Name = "Ryan Reynolds", Photo = "http://i.huffpost.com/gen/3428322/thumbs/o-RYAN-REYNOLDS-TIFF-900.jpg?6", IMDBProfile = "http://www.imdb.com/name/nm0005351/"},
                new Actor() {Name = "Brad Pitt", Photo = "https://s-media-cache-ak0.pinimg.com/originals/12/23/ee/1223eef107b46ef6738f477c3e4c2aed.jpg", IMDBProfile = "http://www.imdb.com/name/nm0000093/"},
                new Actor() {Name = "Jessica Chastain", Photo = "https://pmcdeadline2.files.wordpress.com/2014/07/jessica-chastain.jpg", IMDBProfile = "http://www.imdb.com/name/nm1567113/"},
                new Actor() {Name = "Ben Affleck", Photo = "http://www.hellomagazine.com/imagenes/profiles/ben-affleck/5805-Ben-Affleck.jpg", IMDBProfile = "http://www.imdb.com/name/nm0000255/"},
                new Actor() {Name = "Alexandra Daddario", Photo = "http://vignette1.wikia.nocookie.net/baywatch/images/7/7b/AMPLzYs.jpg/revision/latest?cb=20160110032452", IMDBProfile = "http://www.imdb.com/name/nm1275259/"},
                new Actor() {Name = "Dwayne Johnson", Photo = "https://upload.wikimedia.org/wikipedia/commons/6/6f/Dwayne_Johnson_Hercules_2014_(cropped).jpg", IMDBProfile = "http://www.imdb.com/name/nm0425005/?ref_=tt_ov_st_sm"},
                new Actor() {Name = "Vin Diesel", Photo = "https://images-na.ssl-images-amazon.com/images/M/MV5BMjExNzA4MDYxN15BMl5BanBnXkFtZTcwOTI1MDAxOQ@@._V1_UY317_CR7,0,214,317_AL_.jpg", IMDBProfile = "http://www.imdb.com/name/nm0004874/?ref_=tt_ov_st_sm"},
                new Actor() {Name = "Matt Damon", Photo = "https://upload.wikimedia.org/wikipedia/commons/8/82/Damon_cropped.jpg", IMDBProfile = "http://www.imdb.com/name/nm0000354/?ref_=tt_ov_st_sm"}
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
                    Trailer = "Jlp94-C31cY",
                    Price = 15.99m,
                    Description = "In 1942, a Canadian intelligence officer in North Africa encounters a female French Resistance fighter on a deadly mission behind enemy lines. When they reunite in London, their relationship is tested by the pressures of war.",
                    Country = "UK, USA",
                    Genres = new HashSet<Genre>() {this.genres[0], this.genres[4], this.genres[8] },
                    Actors = new HashSet<Actor>() {this.actors[3], this.actors[6] }
                },
                new Movie()
                {
                    Name = "Inception",
                    Year = 2010,
                    DurationInMinutes = 148,
                    Size = 1.47,
                    Poster = "https://images-na.ssl-images-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_SY1000_CR0,0,675,1000_AL_.jpg",
                    Trailer = "8hP9D6kZseM",
                    Price = 9.99m,
                    Description = "A thief, who steals corporate secrets through use of dream-sharing technology, is given the inverse task of planting an idea into the mind of a CEO.",
                    Country = "UK, USA",
                    Genres = new HashSet<Genre>() {this.genres[0], this.genres[1], this.genres[7], this.genres[9] },
                    Actors = new HashSet<Actor>() {this.actors[3], this.actors[1], this.actors[4] }
                },
                new Movie()
                {
                    Name = "The Dark Knight Rises",
                    Year = 2012,
                    DurationInMinutes = 164,
                    Size = 2.26,
                    Poster = "https://images-na.ssl-images-amazon.com/images/M/MV5BMTk4ODQzNDY3Ml5BMl5BanBnXkFtZTcwODA0NTM4Nw@@._V1_.jpg",
                    Trailer = "GokKUqLcvD8",
                    Price = 1m,
                    Description = "Eight years after the Joker's reign of anarchy, the Dark Knight, with the help of the enigmatic Selina, is forced from his imposed exile to save Gotham City, now on the edge of total annihilation, from the brutal guerrilla terrorist Bane.",
                    Country = "UK, USA",
                    Genres = new HashSet<Genre>() {this.genres[0], this.genres[9] },
                    Actors = new HashSet<Actor>() {this.actors[3], this.actors[1]}
                },
                new Movie()
                {
                    Name = "Ocean's Twelve",
                    Year = 2004,
                    DurationInMinutes = 125,
                    Size = 4.36,
                    Poster = "https://images-na.ssl-images-amazon.com/images/M/MV5BMTQ3NTIyMzczMF5BMl5BanBnXkFtZTYwMjgzNTg2._V1_.jpg",
                    Trailer = "bkYCR677_OQ",
                    Price = 11.99m,
                    Description = "Daniel Ocean recruits one more team member so he can pull off three major European heists in this sequel to Ocean's 11.",
                    Country = "USA",
                    Genres = new HashSet<Genre>() {this.genres[3], this.genres[9] },
                    Actors = new HashSet<Actor>() {this.actors[6], this.actors[12]}
                },
                new Movie()
                {
                    Name = "X-Men Origins: Wolverine",
                    Year = 2009,
                    DurationInMinutes = 103,
                    Size = 4.1,
                    Poster = "https://images-na.ssl-images-amazon.com/images/M/MV5BZWRhMzdhMzEtZTViNy00YWYyLTgxZmUtMTMwMWM0NTEyMjk3XkEyXkFqcGdeQXVyNTIzOTk5ODM@._V1_SY1000_CR0,0,676,1000_AL_.jpg",
                    Trailer = "A6p52S381nE",
                    Price = 13.99m,
                    Description = "A look at Wolverine's early life, in particular his time with the government squad Team X and the impact it will have on his later years.",
                    Country = "UK, USA",
                    Genres = new HashSet<Genre>() {this.genres[0], this.genres[1], this.genres[9], this.genres[5] },
                    Actors = new HashSet<Actor>() {this.actors[0], this.actors[5]}
                },
                new Movie()
                {
                    Name = "Deadpool",
                    Year = 2016,
                    DurationInMinutes = 108,
                    Size = 4.43,
                    Poster = "https://images-na.ssl-images-amazon.com/images/M/MV5BMjQyODg5Njc4N15BMl5BanBnXkFtZTgwMzExMjE3NzE@._V1_SY1000_SX686_AL_.jpg",
                    Trailer = "ONHBaC-pfsk",
                    Price = 19.99m,
                    Description = "A fast-talking mercenary with a morbid sense of humor is subjected to a rogue experiment that leaves him with accelerated healing powers and a quest for revenge.",
                    Country = "USA",
                    Genres = new HashSet<Genre>() {this.genres[0], this.genres[1], this.genres[2]},
                    Actors = new HashSet<Actor>() {this.actors[5]}
                },
                new Movie()
                {
                    Name = "The Departed",
                    Year = 2006,
                    DurationInMinutes = 151,
                    Size = 1.87,
                    Poster = "https://images-na.ssl-images-amazon.com/images/M/MV5BMTI1MTY2OTIxNV5BMl5BanBnXkFtZTYwNjQ4NjY3._V1_.jpg",
                    Trailer = "auYbpnEwBBg",
                    Price = 14.99m,
                    Description = "An undercover cop and a mole in the police attempt to identify each other while infiltrating an Irish gang in South Boston.",
                    Country = "USA, Hong Kong",
                    Genres = new HashSet<Genre>() {this.genres[3], this.genres[4], this.genres[9]},
                    Actors = new HashSet<Actor>() {this.actors[4], this.actors[12]}
                },
                new Movie()
                {
                    Name = "San Andreas",
                    Year = 2015,
                    DurationInMinutes = 114,
                    Size = 4.93,
                    Poster = "https://images-na.ssl-images-amazon.com/images/M/MV5BNjI4MTgyOTAxOV5BMl5BanBnXkFtZTgwMjQwOTA4NTE@._V1_SY1000_SX675_AL_.jpg",
                    Trailer = "yftHosO0eUo",
                    Price = 9.99m,
                    Description = "In the aftermath of a massive earthquake in California, a rescue-chopper pilot makes a dangerous journey with his ex-wife across the state in order to rescue his daughter.",
                    Country = "USA",
                    Genres = new HashSet<Genre>() {this.genres[0], this.genres[1], this.genres[4], this.genres[9] },
                    Actors = new HashSet<Actor>() {this.actors[9], this.actors[10]}
                },
                new Movie()
                {
                    Name = "Furious 7",
                    Year = 2015,
                    DurationInMinutes = 140,
                    Size = 1.46,
                    Poster = "https://images-na.ssl-images-amazon.com/images/M/MV5BMTQxOTA2NDUzOV5BMl5BanBnXkFtZTgwNzY2MTMxMzE@._V1_SY1000_CR0,0,631,1000_AL_.jpg",
                    Trailer = "Skpu5HaVkOc",
                    Price = 14.99m,
                    Description = "Deckard Shaw seeks revenge against Dominic Toretto and his family for his comatose brother.",
                    Country = "USA, Japan",
                    Genres = new HashSet<Genre>() {this.genres[0], this.genres[3], this.genres[9]},
                    Actors = new HashSet<Actor>() {this.actors[11], this.actors[10]}
                },
                new Movie()
                {
                    Name = "The Martian",
                    Year = 2015,
                    DurationInMinutes = 142,
                    Size = 4.43,
                    Poster = "https://images-na.ssl-images-amazon.com/images/M/MV5BMTc2MTQ3MDA1Nl5BMl5BanBnXkFtZTgwODA3OTI4NjE@._V1_SY1000_CR0,0,675,1000_AL_.jpg",
                    Trailer = "Ue4PCI0NamI",
                    Price = 19.99m,
                    Description = "An astronaut becomes stranded on Mars after his team assume him dead, and must rely on his ingenuity to find a way to signal to Earth that he is alive.",
                    Country = "UK, USA",
                    Genres = new HashSet<Genre>() {this.genres[1], this.genres[4], this.genres[5]},
                    Actors = new HashSet<Actor>() {this.actors[12], this.actors[7]}
                },
                new Movie()
                {
                    Name = "Good Will Hunting",
                    Year = 1997,
                    DurationInMinutes = 126,
                    Size = 1.38,
                    Poster = "https://images-na.ssl-images-amazon.com/images/M/MV5BOTI0MzcxMTYtZDVkMy00NjY1LTgyMTYtZmUxN2M3NmQ2NWJhXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_SY1000_CR0,0,655,1000_AL_.jpg",
                    Trailer = "QSMvyuEeIyw",
                    Price = 9.99m,
                    Description = "Will Hunting, a janitor at M.I.T., has a gift for mathematics, but needs help from a psychologist to find direction in his life",
                    Country = "USA",
                    Genres = new HashSet<Genre>() { this.genres[4]},
                    Actors = new HashSet<Actor>() {this.actors[8], this.actors[12]}
                },
                 new Movie()
                {
                    Name = "X-Men: Days of Future Past",
                    Year = 2014,
                    DurationInMinutes = 131,
                    Size = 2.17,
                    Poster = "https://images-na.ssl-images-amazon.com/images/M/MV5BZGIzNWYzN2YtMjcwYS00YjQ3LWI2NjMtOTNiYTUyYjE2MGNkXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_SY1000_CR0,0,675,1000_AL_.jpg",
                    Trailer = "pK2zYHWDZKo",
                    Price = 12.99m,
                    Description = "The X-Men send Wolverine to the past in a desperate effort to change history and prevent an event that results in doom for both humans and mutants.",
                    Country = "USA, UK, Canada",
                    Genres = new HashSet<Genre>() {this.genres[0], this.genres[1], this.genres[9], this.genres[5] },
                    Actors = new HashSet<Actor>() {this.actors[2], this.actors[0]}
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

            var userRoleCreateResult = roleManager.Create(new IdentityRole("User"));

            if (!userRoleCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userRoleCreateResult.Errors));
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
