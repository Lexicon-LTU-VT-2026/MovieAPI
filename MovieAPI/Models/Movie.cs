using System.Collections.Generic;

namespace MovieAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public int Year { get; set; }
        public string Genre { get; set; } = ""; // Normaliseras senare
        public int Duration { get; set; } // I minuter

        // 1:1 med MovieDetails
        public MovieDetail? MovieDetail { get; set; }

        // 1:M med Review
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        // N:M med Actor via MovieActor
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }
}
