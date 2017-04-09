using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DTOs.ViewModels
{
    public class MovieWithCommentsViewModel
    {
        public int Id { get; set; }

        public string MovieName { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
