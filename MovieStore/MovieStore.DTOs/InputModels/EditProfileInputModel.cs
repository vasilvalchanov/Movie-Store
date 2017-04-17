using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.Models.Models;

namespace MovieStore.DTOs.InputModels
{
    public class EditProfileInputModel
    {
        [Required]
        [StringLength(15, ErrorMessage = "The {0} must be between {2} and {1} symbols length")]
        public string Username { get; set; }

        [Required]
        public string Fullname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public static EditProfileInputModel Create(User user)
        {
            return new EditProfileInputModel()
            {
                Username = user.UserName,
                Fullname = user.Fullname,
                Email = user.Email
            };
        }
    }
}
