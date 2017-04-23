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

        private readonly IActorsService actorsService;

        public ActorsController(IActorsService actorService)
        {
            this.actorsService = actorService;
        }

        public ActionResult Index()
        {
            var actors = this.actorsService.GetAllActors();
            return this.View(actors);
        }

    }
}
