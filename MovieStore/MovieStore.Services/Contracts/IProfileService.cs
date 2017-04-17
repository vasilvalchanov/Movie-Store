using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.DTOs.InputModels;

namespace MovieStore.Services.Contracts
{
    public interface IProfileService
    {
        EditProfileInputModel EditProfile(string loggedInUserId, EditProfileInputModel model);

        EditProfileInputModel LoadUserData(string loggedInUserId);
    }
}
