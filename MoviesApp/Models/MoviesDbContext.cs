using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.Models
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options)
        {
            // getAll()
            // Movies.ToList();

        }

        //reference - DB table / repository
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
