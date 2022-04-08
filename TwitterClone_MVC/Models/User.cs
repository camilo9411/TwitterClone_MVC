using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterClone_MVC.Models
{
    public class User
    {

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        [Column("FirtName")]
        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [Required]
        [Column("Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(6)]
        [Column("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Full Name")]
        public string Fullname
        {
            get
            {

                return LastName + ", " + FirstMidName;
            }
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set;}

        public ICollection<Follow> Following { get; set;}

    }
}
