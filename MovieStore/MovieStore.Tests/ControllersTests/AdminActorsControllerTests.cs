using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MovieStore.Areas.Administration.Controllers;
using MovieStore.Controllers;
using MovieStore.Data.Contracts;
using MovieStore.DTOs.InputModels;
using MovieStore.DTOs.ViewModels;
using MovieStore.Services.Contracts;
using MovieStore.Services.Services;
using TestStack.FluentMVCTesting;
using AutoMapper;
using MovieStore.Models.Models;

namespace MovieStore.Tests.ControllersTests
{
    [TestClass]
    public class AdminActorsControllerTests
    {
        private MockContainer mocks;
        private AdminActorsController controller;
        private IActorsService service;
        private Mock<IMovieStoreData> mockContext;

        [TestInitialize]
        public void TestInit()
        {
            this.mocks = new MockContainer();
            this.mocks.PrepareMocks();
            this.mockContext = new Mock<IMovieStoreData>();
            mockContext.Setup(c => c.Actors.All()).Returns(this.mocks.ActorRepositoryMock.Object.All());
            this.service = new ActorsService(mockContext.Object);
            this.controller = new AdminActorsController(this.service);
            this.ConfigureMapper();

        }

        [TestMethod]
        public void Get_CreateActorView_AdminUser_ShouldPass()
        {
            var fakeUser = this.mocks.UserRepositoryMock.Object.All().FirstOrDefault(u => u.Id == "12345");

            this.CheckUserCredentials(fakeUser);

            this.controller.WithCallTo(c => c.Create()).ShouldRenderDefaultView();
        }

        [TestMethod]
        public void Get_CreateActorView_NotAdminUser_ShouldNotPass()
        {
            var fakeUser = this.mocks.UserRepositoryMock.Object.All().FirstOrDefault(u => u.Id == "12345");
            var willTheTestFail = false;
            var message = "Cannot perform test - no users available or current user is not Administrator";

            if (fakeUser == null || !fakeUser.Roles.Any(r => r.RoleId == "2"))
            {
                willTheTestFail = true;
            }

            Assert.IsTrue(willTheTestFail, message);
        }

        [TestMethod]
        public void CreateActor_InvalidModelState_ShouldReturnSameViewWithModel()
        {
            var fakeUser = this.mocks.UserRepositoryMock.Object.All().FirstOrDefault(u => u.Id == "12345");

            this.CheckUserCredentials(fakeUser);

            var newActor = new CreateActorBindingModel()
            {
                Name = null,
                Photo = "New Actor Photo",
                IMDBProfile = "New Actor Profile"
            };
            this.controller.ModelState.AddModelError("err", "name cannot be null");
            this.controller.WithCallTo(c => c.Create(newActor)).ShouldRenderDefaultView()
                .WithModel<CreateActorBindingModel>();
        }

        [TestMethod]
        public void CreateActor_ValidModelState_ShouldCreateIt_AndRedirect()
        {
            var fakeUser = this.mocks.UserRepositoryMock.Object.All().FirstOrDefault(u => u.Id == "12345");

            this.CheckUserCredentials(fakeUser);

            var newActor = new CreateActorBindingModel()
            {
                Name = null,
                Photo = "New Actor Photo",
                IMDBProfile = "New Actor Profile"
            };

            this.controller.WithCallTo(c => c.Create(newActor)).ShouldRedirectToRoute("");


        }

        [TestMethod]
        public void EditActorView_AdminUser_ShouldPass()
        {

            var fakeUser = this.mocks.UserRepositoryMock.Object.All().FirstOrDefault(u => u.Id == "12345");

            this.CheckUserCredentials(fakeUser);

            this.controller.WithCallTo(c => c.Edit(1)).ShouldRenderDefaultView();

        }

        [TestMethod]
        public void EditActorView_InvalidActorId_ShouldReturnNotFound()
        {
            var fakeUser = this.mocks.UserRepositoryMock.Object.All().FirstOrDefault(u => u.Id == "12345");

            this.CheckUserCredentials(fakeUser);

            this.controller.WithCallTo(c => c.Edit(22)).ShouldGiveHttpStatus(HttpStatusCode.NotFound);

        }

        [TestMethod]
        public void EditActorView_AdminUser_NullActorId_ShouldReturnBadRequest()
        {
            var fakeUser = this.mocks.UserRepositoryMock.Object.All().FirstOrDefault(u => u.Id == "12345");

            this.CheckUserCredentials(fakeUser);

            int? id = null;
            this.controller.WithCallTo(c => c.Edit(id)).ShouldGiveHttpStatus(HttpStatusCode.BadRequest);

        }

        [TestMethod]
        public void Get_EditActorView_NotAdminUser_ShouldNotPass()
        {
            var fakeUser = this.mocks.UserRepositoryMock.Object.All().FirstOrDefault(u => u.Id == "12345");
            var willTheTestFail = false;
            var message = "Cannot perform test - no users available or current user is not Administrator";

            if (fakeUser == null || !fakeUser.Roles.Any(r => r.RoleId == "2"))
            {
                willTheTestFail = true;
            }

            Assert.IsTrue(willTheTestFail, message);
        }

        [TestMethod]
        public void EditActorView_ShouldReturnCorrectModelType()
        {
            var fakeUser = this.mocks.UserRepositoryMock.Object.All().FirstOrDefault(u => u.Id == "12345");

            this.CheckUserCredentials(fakeUser);

            this.controller.WithCallTo(c => c.Edit(1)).ShouldRenderDefaultView()
                .WithModel<ActorViewModel>();

        }

        [TestMethod]
        public void EditActorView_ShouldReturnCorrectModel()
        {
            var fakeUser = this.mocks.UserRepositoryMock.Object.All().FirstOrDefault(u => u.Id == "12345");
            var fakeGenres = this.mocks.GenreRepositoryMock.Object.All().ToList();
            var fakeActor = this.mocks.ActorRepositoryMock.Object.All().FirstOrDefault(a => a.Id == 1);
            fakeActor.Movies = new List<Movie>()
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
            };

            this.CheckUserCredentials(fakeUser);


            var actorView = this.mockContext.Object.Actors.All().Where(a => a.Id == 1)
                .Select(ActorViewModel.Create).FirstOrDefault();

            this.controller.WithCallTo(c => c.Edit(1)).ShouldRenderDefaultView()
                .WithModel<ActorViewModel>(a => 
                    a.Id == actorView.Id
                    && a.Name == actorView.Name
                    && a.Photo == actorView.Photo
                    && a.IMDBProfile == actorView.IMDBProfile
                    && a.Movies.Any(m =>  actorView.Movies.Any(am => am.Id == m.Id && am.Name == m.Name)));

        }

        [TestMethod]
        public void EditActorView_ShouldEditIt_AndRedirect()
        {
            var fakeUser = this.mocks.UserRepositoryMock.Object.All().FirstOrDefault(u => u.Id == "12345");
            var fakeGenres = this.mocks.GenreRepositoryMock.Object.All().ToList();
            var fakeActor = this.mocks.ActorRepositoryMock.Object.All().FirstOrDefault(a => a.Id == 1);
            fakeActor.Movies = new List<Movie>()
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
            };

            this.CheckUserCredentials(fakeUser);

            var editActorBM = new EditActorBindingModel()
            {
                Id = 1,
                Name = "Edited Name",
                Photo = "Photo",
                IMDBProfile = "Profile"
            };

            this.controller.WithCallTo(c => c.Edit(editActorBM)).ShouldRedirectToRoute("");

        }

        [TestMethod]
        public void EditActorView_ShouldEditIt_()
        {
            var fakeUser = this.mocks.UserRepositoryMock.Object.All().FirstOrDefault(u => u.Id == "12345");
            var fakeGenres = this.mocks.GenreRepositoryMock.Object.All().ToList();
            var fakeActor = this.mocks.ActorRepositoryMock.Object.All().FirstOrDefault(a => a.Id == 1);
            fakeActor.Movies = new List<Movie>()
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
            };

            this.CheckUserCredentials(fakeUser);

            var editActorBm = new EditActorBindingModel()
            {
                Id = 1,
                Name = "Edited Name",
                Photo = "Photo",
                IMDBProfile = "Profile"
            };

           
            this.controller.WithCallTo(c => c.Edit(editActorBm)).ShouldRedirectToRoute("");

            this.controller.WithCallTo(c => c.Edit(1)).ShouldRenderDefaultView()
                .WithModel<ActorViewModel>(a =>
                    a.Id == 1
                    && a.Name == "Edited Name"
                    && a.Photo == "Photo"
                    && a.IMDBProfile == "Profile");

        }


        private void ConfigureMapper()
        {
            Mapper.Initialize(expression =>
            {
                expression.CreateMap<CreateActorBindingModel, Actor>();
                expression.CreateMap<EditActorBindingModel, Actor>();
            });
        }

        private void CheckUserCredentials(User fakeUser)
        {
            if (fakeUser == null)
            {
                Assert.Fail("Cannot perform test - no users available");
            }

            if (!fakeUser.Roles.Any(r => r.RoleId == "1"))
            {
                Assert.Fail("Cannot perform test - current user is not Administrator");
            }
        }

    }
}
