using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.Models
{
    public class Movie
    { 
        public long Id { get; set; }
        public string Title { get; set; }
        //[MaxLength(10, ErrorMessage="Description must be at most 100 characters")]
        public string Description { get; set; }
        public MovieGenre Genre { get; set; }      
        public string Duration { get; set; }    // durata -> data type?
        public DateTime YearOfRelease { get; set; }   
        public string Director { get; set; }
        public DateTimeOffset DateAdded { get; set; }
        public double Rating { get; set; }
        public bool Watched { get; set; }       //yes,no
    }
}
