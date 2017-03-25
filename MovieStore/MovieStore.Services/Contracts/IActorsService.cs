using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.DTOs.InputModels;
using MovieStore.DTOs.ViewModels;

namespace MovieStore.Services.Contracts
{
    public interface IActorsService
    {
        List<ActorViewModel> GetAllActors();

        void CreateActor(CreateActorBindingModel model);

        void EditActor(EditActorBindingModel model);

        ActorViewModel GetActorViewById(int id);

        void Delete(int id);
    }
}
