using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TwitterClone_MVC.Models
{
    public class Like
    {
        
        public int TweetID { get; set; }
        public int UserID { get; set; }
        public Tweet Tweet { get; set; }
        public User User { get; set; }


    }
}
