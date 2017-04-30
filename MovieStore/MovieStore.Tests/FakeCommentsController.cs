using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MovieStore.Controllers;
using MovieStore.Services.Contracts;

namespace MovieStore.Tests
{
    public class FakeCommentsController : CommentsController
    {
        public FakeCommentsController(ICommentsService commentsService) : base(commentsService)
        {
        }

        protected override string LoggedInUserId { get; } = "12345";
    }
}
