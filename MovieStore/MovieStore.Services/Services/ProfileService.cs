using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MovieStore.Data.Contracts;
using MovieStore.DTOs.InputModels;
using MovieStore.Models.Models;
using MovieStore.Services.Contracts;

namespace MovieStore.Services.Services
{
    public class ProfileService : BaseService, IProfileService
    {
        public ProfileService(IMovieStoreData data) : base(data)
        {
        }

        public EditProfileInputModel EditProfile(string loggedInUserId, EditProfileInputModel model)
        {
            this.CheckModelForNull(model);

            var user = this.GetUserById(loggedInUserId);

            user.Fullname = model.Fullname;
            user.Email = model.Email;
            user.UserName = model.Username;
            Mapper.Map(model, user);

            this.Data.SaveChanges();

            var viewModel = Mapper.Map<EditProfileInputModel>(user);

            return viewModel;
        }

        public EditProfileInputModel LoadUserData(string loggedInUserId)
        {
             var user = this.GetUserById(loggedInUserId);

            var viewModel = Mapper.Map<EditProfileInputModel>(user);
            return viewModel;
        }

        private User GetUserById(string loggedInUserId)
        {
            var user = this.Data.Users.Find(loggedInUserId);

            if (user == null)
            {
                throw new ArgumentException("There is not such user");
            }

            return user;
        }
    }
}
