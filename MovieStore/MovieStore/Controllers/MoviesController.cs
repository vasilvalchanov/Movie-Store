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
using MovieStore.Extensions;
using MovieStore.Models.Models;
using MovieStore.Services.Contracts;

namespace MovieStore.Controllers
{
    public class MoviesController : Controller
    {
        private MovieStoreContext db = new MovieStoreContext();

        private readonly IMovieService movieService;
        private IActorsService actorService;

        public MoviesController(IMovieService movieService, IActorsService actorsService)
        {
            this.movieService = movieService;
            this.actorService = actorsService;
        }

        // GET: Movies
        public ActionResult Index()
        {
            var movies = this.movieService.GetAllMovies();
            return this.View(movies);
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
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
                this.AddNotification(ex.Message, NotificationType.ERROR);
                return RedirectToAction("Index");
            }
            
        }

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
                return RedirectToAction("Index");
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
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    this.AddNotification(ex.Message, NotificationType.ERROR);
                    return this.View(model);
                }
            }

            return this.View(model);
        }

        // MOve to Service
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
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    this.AddNotification(ex.Message, NotificationType.ERROR);
                    return this.View();
                }
            }

            return View();
        }

        // GET: Movies/Delete/5
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

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                this.movieService.Delete(id);
                this.AddNotification("Deleted successfully", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.AddNotification(ex.Message, NotificationType.ERROR);
                return this.View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
