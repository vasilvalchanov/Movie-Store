﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using MovieStore.Models.Models;
using MovieStore.Services.Contracts;
using MovieStore.Services.Services;

namespace MovieStore.Controllers
{
    public class BaseController : Controller
    {

        protected string LoggedInUserId => this.User.Identity.GetUserId();
        protected string LoggedInUserName => this.User.Identity.GetUserName();
        protected bool IsLoggedInUserAdmin => this.User.IsInRole("Administrator");
    }
}