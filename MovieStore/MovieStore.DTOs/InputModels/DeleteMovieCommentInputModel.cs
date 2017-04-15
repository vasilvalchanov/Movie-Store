using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DTOs.InputModels
{
    public class DeleteMovieCommentInputModel
    {
        [Required]
        public int CommentId { get; set; }

        [Required]
        public int MovieId { get; set; }
    }
}
