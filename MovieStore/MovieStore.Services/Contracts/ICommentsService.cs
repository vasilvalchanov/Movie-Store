using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.DTOs.InputModels;
using MovieStore.DTOs.ViewModels;
using MovieStore.Models.Models;

namespace MovieStore.Services.Contracts
{
    public interface ICommentsService
    {
        MovieWithCommentsViewModel GetAllCommentsByMovieId(int movieId);

        CommentViewModel CommentMovie(CreateCommentInputModel model, string loggedInUserId);

        void DeleteComment(int id, string loggedInUserId, bool isLoggedInUserAdmin);

        CommentViewModel GetCommentById(int id);
    }
}
