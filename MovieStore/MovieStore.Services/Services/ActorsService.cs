using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MovieStore.Data.Contracts;
using MovieStore.Services.Contracts;
using MovieStore.DTOs.ViewModels;
using MovieStore.DTOs.InputModels;
using MovieStore.Models.Models;

namespace MovieStore.Services.Services
{
    public class ActorsService : BaseService, IActorsService
    {
        public ActorsService(IMovieStoreData data) : base(data)
        {
        }

        public void CreateActor(CreateActorBindingModel model)
        {
            this.CheckModelForNull(model);
            this.CheckActorForDuplication(model);

            var actor = Mapper.Map<Actor>(model);

            this.Data.Actors.Add(actor);
            this.Data.SaveChanges();
        }

        public List<ActorViewModel> GetAllActors()
        {
            var actors = this.Data.Actors.All()
                .Include(a => a.Movies)
                .Select(ActorViewModel.Create).ToList();

            //var actors = this.Data.Actors.All()
            //    .Include(a => a.Movies);

            //var viewModels = Mapper.Map<IEnumerable<ActorViewModel>>(actors);

            return actors;
        }

        public void EditActor(EditActorBindingModel model)
        {
            this.CheckModelForNull(model);
            this.CheckActorForDuplication(model);
            var actor = this.GetActorById(model.Id);

            //actor.Name = model.Name;
            //actor.Photo = model.Photo;
            //actor.IMDBProfile = model.IMDBProfile;

            Mapper.Map(model, actor);

            this.Data.SaveChanges();
        }

        public ActorViewModel GetActorViewById(int id)
        {
            var actor = this.Data.Actors.All()
                .Where(a => a.Id == id)
                .Select(ActorViewModel.Create)
                .FirstOrDefault();

            if (actor == null)
            {
                throw new ArgumentNullException("There is not such actor in database");
            }

            return actor;
        }

        public void Delete(int id)
        {
            var actor = this.GetActorById(id);
            this.Data.Actors.Delete(actor);
            this.Data.SaveChanges();
        }

        private Actor GetActorById(int id)
        {
            var actor = this.Data.Actors.All()
                .FirstOrDefault(a => a.Id == id);

            if (actor == null)
            {
                throw new ArgumentNullException("There is not such actor in database");
            }

            return actor;
        }

        private void CheckActorForDuplication(CreateActorBindingModel model)
        {
            if (this.Data.Actors.All().Any(a => a.Name == model.Name))
            {
                throw new InvalidOperationException($"There is already actor with name {model.Name}");
            }
        }

        private void CheckActorForDuplication(EditActorBindingModel model)
        {
            if (this.Data.Actors.All().Any(a => a.Name == model.Name && a.Id != model.Id))
            {
                throw new InvalidOperationException($"There is already actor with name {model.Name}");
            }
        }
    }
}
