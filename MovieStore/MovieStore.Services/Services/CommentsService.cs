using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.Data.Contracts;
using MovieStore.DTOs.InputModels;
using MovieStore.DTOs.ViewModels;
using MovieStore.Models.Models;
using MovieStore.Services.Contracts;

namespace MovieStore.Services.Services
{
    public class CommentsService : BaseService, ICommentsService
    {
        public CommentsService(IMovieStoreData data) : base(data)
        {
        }

        public MovieWithCommentsViewModel GetAllCommentsByMovieId(int movieId)
        {
            var movieWithComments = new MovieWithCommentsViewModel();

            var movie = this.Data.Movies.All()
                .Include(m => m.Comments)
                .Include("Comments.Author")
                .FirstOrDefault(m => m.Id == movieId);

            if (movie == null)
            {
                throw new ArgumentNullException("There is not movie with such id");
            }

            var comments = movie.Comments.AsQueryable()
                .OrderByDescending(c => c.CreatedAt)
                .Select(CommentViewModel.Create);
            movieWithComments.Id = movieId;
            movieWithComments.MovieName = movie.Name;
            movieWithComments.Comments = comments;

            return movieWithComments;
        }

        public CommentViewModel CommentMovie(CreateCommentInputModel model, string loggedInUserId)
        {
            this.CheckModelForNull(model);

            var comment = new Comment()
            {
                Content = model.Content,
                MovieId = model.MovieId,
                AuthorId = loggedInUserId,
                CreatedAt = DateTime.Now
            };

            this.Data.Comments.Add(comment);
            this.Data.SaveChanges();

            var commentToReturn = this.Data.Comments.All()
                .Where(c => c.Id == comment.Id)
                .OrderByDescending(c => c.CreatedAt)
                .Select(CommentViewModel.Create)
                .FirstOrDefault();

            return commentToReturn;
        }

        public void DeleteComment(int id, string loggedInUserId, bool isLoggedInUserAdmin)
        {
            var comment = this.Data.Comments.All()
               .FirstOrDefault(c => c.Id == id);

            if (comment == null)
            {
                throw new ArgumentNullException("There is not comment with such id");
            }

            if (isLoggedInUserAdmin || comment.AuthorId == loggedInUserId)
            {
                this.Data.Comments.Delete(comment);
                this.Data.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("You can delete only comments created by you");
            }
        }

        public CommentViewModel GetCommentById(int id)
        {
            var comment = this.Data.Comments.All()
                .Where(c => c.Id == id)
                .Select(CommentViewModel.Create)
                .FirstOrDefault();

            if (comment == null)
            {
                throw new ArgumentException("There is not comment with such id");
            }

            return comment;
        }

    }
}
