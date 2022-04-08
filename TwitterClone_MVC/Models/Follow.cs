using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterClone_MVC.Models
{
    public class Follow
    {

        public int FollowID { get; set; }
        public int UserID { get; set; }
        public int FollowingID { get; set; }

        public User User { get; set; }
        public User Following { get; set; }

    }
}
