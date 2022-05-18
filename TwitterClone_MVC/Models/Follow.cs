namespace TwitterClone_MVC.Models
{
    public class Follow
    {
        public int UserID { get; set; }
        public int FollowingID { get; set; }
        public User Following { get; set; }

    }
}
