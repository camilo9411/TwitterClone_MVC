using Microsoft.EntityFrameworkCore;
using TwitterClone_MVC.Models;

namespace TwitterClone_MVC.Data
{
    public class TwitterContext: DbContext
    {


        public TwitterContext(DbContextOptions<TwitterContext> options) : base(options)
        {
        }

        //public DbSet<Course> Courses { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Follow>().ToTable("Enrollment");
            modelBuilder.Entity<User>().ToTable("Student");
        }


    }
}
