using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MovieStore.Controllers;
using MovieStore.Data.Contracts;
using MovieStore.DTOs.ViewModels;
using MovieStore.Models.Models;
using MovieStore.Services.Contracts;
using MovieStore.Services.Services;
using TestStack.FluentMVCTesting;

namespace MovieStore.Tests.ControllersTests
{
    [TestClass]
    public class ActorsControllerTests
    {
        private MockContainer mocks;
        private ActorsController controller;
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
            this.controller = new ActorsController(this.service);

            var fakeUser = this.mocks.UserRepositoryMock.Object.All().FirstOrDefault();

            if (fakeUser == null)
            {
                Assert.Fail("Cannot perform test - no users available");
            }

        }

        [TestMethod]
        public void Index_ShouldPass()
        {
            this.controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView();
        }

        [TestMethod]
        public void Index_ShouldReturnCorrectModelType()
        {
            this.controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView().WithModel<List<ActorViewModel>>();
        }

        [TestMethod]
        public void Index_ShouldReturnCorrectActorsCount()
        {
            this.controller.WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel<List<ActorViewModel>>(m => m.Count == 3);
        }

        [TestMethod]
        public void Index_ShouldReturnCorrectActorModels()
        {
            var expectedActors = new List<ActorViewModel>()
            {
                new ActorViewModel()
                {
                    Id = 1,
                    IMDBProfile = "Test Profile 1",
                    Name = "Test Name 1",
                    Photo = "Test Photo 1"
                },
                new ActorViewModel()
                {
                    Id = 2,
                    IMDBProfile = "Test Profile 2",
                    Name = "Test Name 2",
                    Photo = "Test Photo 2"
                },
                new ActorViewModel()
                {
                    Id = 3,
                    IMDBProfile = "Test Profile 3",
                    Name = "Test Name 3",
                    Photo = "Test Photo 3"
                }
            };

            this.controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView()
                .WithModel<List<ActorViewModel>>(m =>  
                    m[0].Id == expectedActors[0].Id 
                    && m[0].Name == expectedActors[0].Name 
                    && m[0].IMDBProfile == expectedActors[0].IMDBProfile
                    && m[0].Photo == expectedActors[0].Photo);

            this.controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView()
                .WithModel<List<ActorViewModel>>(m =>
                    m[1].Id == expectedActors[1].Id
                    && m[1].Name == expectedActors[1].Name
                    && m[1].IMDBProfile == expectedActors[1].IMDBProfile
                    && m[1].Photo == expectedActors[1].Photo);

            this.controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView()
                .WithModel<List<ActorViewModel>>(m =>
                    m[2].Id == expectedActors[2].Id
                    && m[2].Name == expectedActors[2].Name
                    && m[2].IMDBProfile == expectedActors[2].IMDBProfile
                    && m[2].Photo == expectedActors[2].Photo);
        }
    }
}
