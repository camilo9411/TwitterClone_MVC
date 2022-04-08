using TwitterClone_MVC.Data;
using TwitterClone_MVC.Models;
using System;
using System.Linq;

namespace TwitterClone_MVC.Models
{
    public class DbInitializer
    {

            public static void Initialize(TwitterContext context)
            {
                context.Database.EnsureCreated();

                // Look for any students.
                if (context.Users.Any())
                {
                    return;   // DB has been seeded
                }

                var users = new User[]
                {
                    new User{FirstMidName="Carson",LastName="Alexander",Password = "123456", Email = "carson@me.com",EnrollmentDate=DateTime.Parse("2000-09-01")},
                    new User{FirstMidName="Camilo",LastName="Restrepo",Password = "123456", Email = "camilo@me.com",EnrollmentDate=DateTime.Parse("2002-09-01")},
                    new User{FirstMidName="Andres",LastName="Ardila",Password = "123456", Email = "andres@me.com",EnrollmentDate=DateTime.Parse("2003-09-01")},

                };
                foreach (User u in users)
                {
                    context.Users.Add(u);
                }
                context.SaveChanges();

                var follows = new Follow[]
                {
                        new Follow{UserID = 1, FollowingID = 2},
                        new Follow{UserID = 2, FollowingID = 3},

                 };
                foreach (Follow f in follows)
                {
                    context.Follows.Add(f);
                }
                context.SaveChanges();
        }
        }


}
