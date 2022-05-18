using Microsoft.EntityFrameworkCore;
using TwitterClone_MVC.Models;

namespace TwitterClone_MVC.Data
{
    public class TwitterContext: DbContext
    {


        public TwitterContext(DbContextOptions<TwitterContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<Like> Likes { get; set; }

        public DbSet<Follow> Follow { get; set; }
        public DbSet<Comment> Comment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Like>()
                .HasKey(t => new { t.TweetID, t.UserID });


            modelBuilder.Entity<Comment>().HasOne(c => c.User)
                .WithMany(l => l.Comments)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Follow>()
                .HasKey(t => new { t.FollowingID, t.UserID });

            modelBuilder.Entity<Like>().HasOne(u => u.User)
                .WithMany(l => l.Likes)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>().ToTable("User");    
            modelBuilder.Entity<Tweet>().ToTable("Tweet");
            modelBuilder.Entity<Like>().ToTable("Like");


        }








    }
}
