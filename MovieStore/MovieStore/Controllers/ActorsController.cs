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
    public class ActorsController : Controller
    {
        private MovieStoreContext db = new MovieStoreContext();

        private readonly IActorsService actorsService;

        public ActorsController(IActorsService actorService)
        {
            this.actorsService = actorService;
        }

        // GET: Actors
        public ActionResult Index()
        {
            var actors = this.actorsService.GetAllActors();
            return this.View(actors);
        }

        // GET: Actors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

        // GET: Actors/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateActorBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    this.actorsService.CreateActor(model);
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

        // GET: Actors/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var actor = this.actorsService.GetActorViewById(id.Value);
                return this.View(actor);
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditActorBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    this.actorsService.EditActor(model);
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

        // GET: Actors/Delete/5
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var actor = this.actorsService.GetActorViewById(id.Value);
                return this.View(actor);
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message);
            }

        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                this.actorsService.Delete(id);
                this.AddNotification("Deleted successfully", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.AddNotification(ex.Message, NotificationType.ERROR);
                return this.View();
            }

        }
    }
}
