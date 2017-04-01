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

        private IMovieService movieService;
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
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        //MOVE TO SERVICE
        public ActionResult Create()
        {
            var model = new CreateMovieBindingModel();
            var actors = this.actorService.GetAllActors().ToList();
            var genres = this.movieService.GetAllMovieGenres().ToList();
            var actorItems = actors.Select(actor => new SelectListItem()
            {
                Value = actor.Id.ToString(),
                Text = actor.Name
            }).ToList();

            var genreItems = genres.Select(genre => new SelectListItem()
            {
                Value = genre.Id.ToString(),
                Text = genre.Name
            }).ToList();

            MultiSelectList actorsList = new MultiSelectList(actorItems.OrderBy(i => i.Text), "Value", "Text");
            MultiSelectList genreList = new MultiSelectList(genreItems.OrderBy(i => i.Text), "Value", "Text");

            model.Actors = actorsList;
            model.Genres = genreList;

            return View(model);
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
                var movie = this.movieService.GetMovieViewById(id.Value);
                //var model = new CreateMovieBindingModel();
                var actors = this.actorService.GetAllActors().ToList();
                var genres = this.movieService.GetAllMovieGenres().ToList();
                var selectedActorsIds = new List<string>();
                var selectedGenresIds = new List<string>();
                selectedActorsIds.AddRange(movie.Actors.Select(a => a.Id.ToString()));
                selectedGenresIds.AddRange(movie.Genres.Select(a => a.Id.ToString()));
                var actorItems = actors.Select(actor => new SelectListItem()
                {
                    Value = actor.Id.ToString(),
                    Text = actor.Name
                }).ToList();

                var genreItems = genres.Select(genre => new SelectListItem()
                {
                    Value = genre.Id.ToString(),
                    Text = genre.Name
                }).ToList();

                MultiSelectList actorsList = new MultiSelectList(actorItems.OrderBy(i => i.Text), "Value", "Text");
                MultiSelectList genreList = new MultiSelectList(genreItems.OrderBy(i => i.Text), "Value", "Text");

                movie.ActorsSelectList = new MultiSelectList(actorsList, "Value", "Text", selectedActorsIds);
                movie.GenresSelectList = new MultiSelectList(genreList, "Value", "Text", selectedGenresIds);
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
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
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
