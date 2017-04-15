using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieStore.Controllers;
using MovieStore.DTOs.InputModels;
using MovieStore.Extensions;
using MovieStore.Services.Contracts;

namespace MovieStore.Areas.Administration.Controllers
{
    [Authorize(Roles = "Administrator")]
    [RouteArea("Administration")]
    public class AdminMoviesController : BaseController
    {
        private readonly IMovieService movieService;

        public AdminMoviesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                var model = this.movieService.LoadCreateMovieData();
                return View(model);
            }
            catch (Exception ex)
            {
                this.AddNotification(ex.Message, NotificationType.ERROR);
                return RedirectToAction("Index", "Movies", new { Area = "" });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateMovieBindingModel model)
        {

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.movieService.CreateMovie(model);
                    this.AddNotification("Created successfully", NotificationType.SUCCESS);
                    return RedirectToAction("Index", "Movies", new { Area = "" });
                }
                catch (Exception ex)
                {
                    this.AddNotification(ex.Message, NotificationType.ERROR);
                    return this.View(model);
                }
            }

            return this.View(model);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var movie = this.movieService.LoadEditMovieData(id.Value);
                return this.View(movie);
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditMovieBindingModel model)
        {

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.movieService.EditMovie(model);
                    this.AddNotification("Edited successfully", NotificationType.SUCCESS);
                    return RedirectToAction("Index", "Movies", new {Area = ""});
                }
                catch (Exception ex)
                {
                    this.AddNotification(ex.Message, NotificationType.ERROR);
                    return this.View();
                }
            }

            return View();
        }


        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var movie = this.movieService.GetMovieViewById(id.Value);
                return this.View(movie);
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                this.movieService.Delete(id);
                this.AddNotification("Deleted successfully", NotificationType.SUCCESS);
                return RedirectToAction("Index", "Movies", new { Area = "" });
            }
            catch (Exception ex)
            {
                this.AddNotification(ex.Message, NotificationType.ERROR);
                return this.View();
            }
        }
    }
}