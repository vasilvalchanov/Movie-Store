using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieStore.Data;
using MovieStore.DTOs.InputModels;
using MovieStore.DTOs.ViewModels;
using MovieStore.Extensions;
using MovieStore.Models.Models;
using MovieStore.Services.Contracts;

namespace MovieStore.Controllers
{
    public class CommentsController : BaseController
    {
        private ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }


        [HttpGet]
        [Route("{id}")]
        public ActionResult MovieComments(int id)
        {
            var comments = this.commentsService.GetAllCommentsByMovieId(id);
            return View(comments);
        }


        public ActionResult Create()
        {         
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCommentInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    var comment = commentsService.CommentMovie(model, this.LoggedInUserId);
                    return PartialView("_CommentPartial", comment);
                }
                catch (Exception ex)
                {
                    this.AddNotification(ex.Message, NotificationType.ERROR);
                    return PartialView("_CreateCommentPartial", model.MovieId);
                }
            }

            return PartialView("_CreateCommentPartial", model.MovieId);
        }


        public ActionResult Delete(CommentViewModel model)
        {
            try
            {
                var comment = this.commentsService.GetCommentById(model.Id);
                return this.View(comment);
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(CommentViewModel model)
        {

            try
            {
                this.commentsService.DeleteComment(model.Id, this.LoggedInUserId, this.IsLoggedInUserAdmin);
                this.AddNotification("Comment deleted successfully", NotificationType.SUCCESS);
                return this.RedirectToAction("MovieComments", new { id = model.MovieId});
            }
            catch (Exception ex)
            {
                this.AddNotification(ex.Message, NotificationType.ERROR);
                return this.RedirectToAction("MovieComments", new { id = model.MovieId });
            }        
        }

    }
}
