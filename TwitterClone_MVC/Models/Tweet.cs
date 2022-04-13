using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TwitterClone_MVC.Models
{
    public class Tweet
    {

        public int ID { get; set; }

        public int UserID { get; set; }

        [Required(ErrorMessage = "*Tweet is required")]
        [StringLength(140, MinimumLength = 3)]
        public string Message { get; set; }

    
        public User User { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:G}", ApplyFormatInEditMode = true)]
        public DateTime CreatedOn { get; set; }

        public ICollection<Like> Likes { get; set;}
        public ICollection<Comment> Comments { get; set; }

    }
}
