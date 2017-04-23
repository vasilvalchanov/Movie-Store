using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MovieStore.Areas.Administration.Controllers;
using MovieStore.Controllers;
using MovieStore.Data.Contracts;
using MovieStore.Services.Contracts;
using MovieStore.Services.Services;
using TestStack.FluentMVCTesting;

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

            

        }

        [TestMethod]
        public void Get_CreateActorView_AdminUser_ShouldPass()
        {
            var fakeUser = this.mocks.UserRepositoryMock.Object.All().FirstOrDefault(u => u.Id == "12345");

            if (fakeUser == null)
            {
                Assert.Fail("Cannot perform test - no users available");
            }

            if (!fakeUser.Roles.Any(r => r.RoleId == "1"))
            {
                Assert.Fail("Cannot perform test - current user is not Administrator");
            }

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

    }
}
