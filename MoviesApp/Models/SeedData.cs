using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MoviesDbContext(serviceProvider.GetRequiredService<DbContextOptions<MoviesDbContext>>()))
            {
                // Look for any movies.
                if (context.Movies.Any())
                {
                    return;   // DB table has been seeded
                }

                context.Movies.AddRange(
                    new Movie
                    {
                        Title = "The Terminator",
                        Description = "a human soldier tasked to stop an indestructible cyborg killing machine",
                        Genre = 0,
                        Duration = "107",
                        YearOfRelease = DateTime.Parse("10/26/1984"),
                        Director = "James Cameron",
                        DateAdded = DateTime.Now,
                        Rating = 8.0,
                        Watched = true
                    },
                    new Movie
                    {
                        Title = "Watchmen",
                        Description = "a group of mostly retired American superheroes investigates the murder of one of their own",
                        Genre = 0,
                        Duration = "162",
                        YearOfRelease = DateTime.Parse("02/23/2009"),
                        Director = "Zack Snyder",
                        DateAdded = DateTime.Now,
                        Rating = 7.6,
                        Watched = true
                    }

                );
                context.SaveChanges();
            }
        }
    }
}
