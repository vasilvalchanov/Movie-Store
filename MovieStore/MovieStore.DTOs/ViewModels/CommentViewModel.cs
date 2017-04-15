using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MovieStore.Models.Models;

namespace MovieStore.DTOs.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Content { get; set; }

        public int MovieId { get; set; }

        public string UserId { get; set; }

        public string User { get; set; }

        public static Expression<Func<Comment, CommentViewModel>> Create
        {
            get
            {
                return comment => new CommentViewModel()
                {
                    Id = comment.Id,
                    CreatedAt = comment.CreatedAt,
                    UserId = comment.AuthorId,
                    User = comment.Author.Fullname,
                    Content = comment.Content,
                    MovieId = comment.MovieId
                };
            }
        } 
    }
}
