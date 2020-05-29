using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.Models
{
    public class Movie
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public MovieGenre Genre { get; set; }      
        public string Duration { get; set; }    // durata -> metoda de masurare?
        public DateTime YearOfRelease { get; set; }   
        public string Director { get; set; }
        public DateTimeOffset DateAdded { get; set; }
        public double Rating { get; set; }
        public bool Watched { get; set; }       //yes,no
    }
}
