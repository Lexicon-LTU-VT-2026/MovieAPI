using System.Collections.Generic;

namespace MovieAPI.DTOs
{
    public class MovieDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public int Year { get; set; }
        public string Genre { get; set; } = "";
        public int Duration { get; set; }

        // MovieDetails
        public string Synopsis { get; set; } = "";
        public string Language { get; set; } = "";
        public decimal Budget { get; set; }

        // Relationer
        public List<ReviewDto> Reviews { get; set; } = new();
        public List<ActorDto> Actors { get; set; } = new();
    }
}
