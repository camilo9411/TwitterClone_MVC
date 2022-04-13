using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterClone_MVC.Models
{
    [Table("User")]
    public class User
    {

        public int ID { get; set; }

        [Required(ErrorMessage = "*FirstName is required")]
        [StringLength(50, MinimumLength = 3)]
        [Column("FirtName")]
        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [Required(ErrorMessage = "*LastName is required")]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "*Email is required")]
        [Column("Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*Password is required")]
        [StringLength(50, MinimumLength = 3)]
        [Column("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Full Name")]
        public string Fullname
        {
            get
            {

                return FirstMidName + " " + LastName;
            }
        }

        public ICollection<Tweet> Tweets { get; set;}
        public ICollection<Like> Likes { get; set; }

        public ICollection<Follow> Follows { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
