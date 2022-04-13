namespace TwitterClone_MVC.Models
{
    public class Comment
    {


        public int TweetID { get; set; }
        public int UserID { get; set; }

        public string Message { get; set; }
        public Tweet Tweet { get; set; }
        public User User { get; set; }


        

    }
}
