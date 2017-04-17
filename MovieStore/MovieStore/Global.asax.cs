using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.UI.WebControls.WebParts;
using MovieStore.Models.Models;
using AutoMapper;
using MovieStore.DTOs.InputModels;
using MovieStore.DTOs.ViewModels;

namespace MovieStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            this.ConfigureMapper();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void ConfigureMapper()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<EditProfileInputModel, User>().ReverseMap();
                config.CreateMap<Actor, ActorViewModel>();
                config.CreateMap<CreateActorBindingModel, Actor>();
                config.CreateMap<EditActorBindingModel, Actor>();
                config.CreateMap<RateMovieInputModel, Rating>();
            });
        }
    }
}
