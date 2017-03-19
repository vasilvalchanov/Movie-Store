using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Models.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }
    }
}
