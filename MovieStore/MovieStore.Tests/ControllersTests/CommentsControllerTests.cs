using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MovieStore.Controllers;
using MovieStore.Data.Contracts;
using MovieStore.DTOs.InputModels;
using MovieStore.DTOs.ViewModels;
using MovieStore.Models.Models;
using MovieStore.Services.Contracts;
using MovieStore.Services.Services;
using TestStack.FluentMVCTesting;

namespace MovieStore.Tests.ControllersTests
{
    [TestClass]
    public class CommentsControllerTests
    {
        private MockContainer mocks;
        private FakeCommentsController controller;
        private ICommentsService service;
        private Mock<IMovieStoreData> mockContext;

        [TestInitialize]
        public void TestInit()
        {
            this.mocks = new MockContainer();
            this.mocks.PrepareMocks();
            this.mockContext = new Mock<IMovieStoreData>();
            mockContext.Setup(c => c.Comments.All()).Returns(this.mocks.CommentRepositoryMock.Object.All());
            mockContext.Setup(c => c.Movies.All()).Returns(this.mocks.MovieRepositoryMock.Object.All());
            this.service = new CommentsService(mockContext.Object);
            this.controller = new FakeCommentsController(this.service);

            var fakeUser = this.mocks.UserRepositoryMock.Object.All().FirstOrDefault();

            if (fakeUser == null)
            {
                Assert.Fail("Cannot perform test - no users available");
            }

        }

        [TestMethod]
        public void GetMovieComments_ReturnDefaultView_ShouldPass()
        {
            var fakeUser = this.mocks.UserRepositoryMock.Object.All().FirstOrDefault(u => u.Id == "12345");

            this.CheckUserCredentials(fakeUser);

            this.controller.WithCallTo(c => c.MovieComments(1)).ShouldRenderDefaultView();
        }

        [TestMethod]
        public void GetMovieComments_ShouldReturnCorrectModels()
        {
            var fakeUser = this.mocks.UserRepositoryMock.Object.All().FirstOrDefault(u => u.Id == "12345");

            this.CheckUserCredentials(fakeUser);

            this.controller.WithCallTo(c => c.MovieComments(1)).ShouldRenderDefaultView().WithModel<MovieWithCommentsViewModel>();
        }

        [TestMethod]
        public void GetMovieComments_ShouldReturnCommentsCountCorrect()
        {
            var fakeUser = this.mocks.UserRepositoryMock.Object.All().FirstOrDefault(u => u.Id == "12345");

            this.CheckUserCredentials(fakeUser);

            this.controller.WithCallTo(c => c.MovieComments(1))
                .ShouldRenderDefaultView()
                .WithModel<MovieWithCommentsViewModel>(c => c.Comments.Count() == 2);
        }

        [TestMethod]
        public void GetMovieComments_ShouldReturnCommentsCorrect()
        {
            var fakeUser = this.mocks.UserRepositoryMock.Object.All().FirstOrDefault(u => u.Id == "12345");

            this.CheckUserCredentials(fakeUser);

            this.controller.WithCallTo(c => c.MovieComments(1))
                .ShouldRenderDefaultView()
                .WithModel<MovieWithCommentsViewModel>(
                    cm => cm.Comments.Any(c => c.Id == 1 && c.Content == "Test Content 1" && c.CreatedAt == new DateTime(2017, 05, 05) && c.UserId == "123"));

            this.controller.WithCallTo(c => c.MovieComments(1))
                .ShouldRenderDefaultView()
                .WithModel<MovieWithCommentsViewModel>(
                    cm => cm.Comments.Any(c => c.Id == 2 && c.Content == "Test Content 2" && c.CreatedAt == new DateTime(2017, 05, 06) && c.UserId == "123"));

        }

        [TestMethod]
        public void GetMovieComments_NoComments_ShouldReturnEmptyCommentsCollection()
        {
            var fakeUser = this.mocks.UserRepositoryMock.Object.All().FirstOrDefault(u => u.Id == "12345");
            
            this.CheckUserCredentials(fakeUser);

            this.controller.WithCallTo(c => c.MovieComments(2))
                .ShouldRenderDefaultView()
                .WithModel<MovieWithCommentsViewModel>(c => !c.Comments.Any());
        }

        [TestMethod]
        public void Get_MovieComments_NoLoggedUser_ShouldNotPass()
        {
            var fakeUser = this.mocks.UserRepositoryMock.Object.All().FirstOrDefault(u => u.Id == "12345678");
            var willTheTestFail = false;
            var message = "Cannot perform test - no users available or current user is not Administrator";

            if (fakeUser == null)
            {
                willTheTestFail = true;
            }

            Assert.IsTrue(willTheTestFail, message);
        }

        [TestMethod]
        public void CommentMovie_ValidData_ShouldCommentPartialView()
        {
            var fakeUser = this.mocks.UserRepositoryMock.Object.All().FirstOrDefault(u => u.Id == "12345");
            var fakeComments = new List<Comment>()
            {
               new Comment() { Id = 1, CreatedAt = new DateTime(2017, 04, 23), Content = "Comment 1"},
               new Comment() { Id = 2, CreatedAt = new DateTime(2017, 04, 24), Content = "Comment 2"},
               new Comment() { Id = 3, CreatedAt = new DateTime(2017, 04, 23), Content = "Comment 3"}
            };

            this.mocks.CommentRepositoryMock = new Mock<IRepository<Comment>>();
            this.mocks.CommentRepositoryMock.Setup(r => r.All()).Returns(fakeComments.AsQueryable());
            fakeComments.Add(new Comment() { Id = 0, CreatedAt = new DateTime(2017, 05, 07), Content = "Test Content 4" , MovieId = 1, AuthorId = "12345"});
            this.CheckUserCredentials(fakeUser);

            var commentInput = new CreateCommentInputModel()
            {
                MovieId = 1,
                Content = "Test Content 4"
            };

            this.controller.WithCallTo(c => c.Create(commentInput)).ShouldRenderPartialView("_CommentPartial");
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
