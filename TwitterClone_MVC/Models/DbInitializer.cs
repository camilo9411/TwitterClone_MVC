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
                        new User{FirstMidName = "Carson", LastName = "Alexander", Password = "carson", Email = "carson@me.com" },
                        new User{FirstMidName="Camilo",LastName="Restrepo",Password = "camilo", Email = "camilo@me.com"},
                        new User{FirstMidName="Andres",LastName="Ardila",Password = "andres", Email = "andres@me.com"}

                    };

                foreach (User u in users)
                {
                    context.Users.Add(u);
                }
                context.SaveChanges();

        }


    }


}
