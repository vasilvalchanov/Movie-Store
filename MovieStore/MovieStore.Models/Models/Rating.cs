using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Models.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Range(0, 10)]
        public int Stars { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
