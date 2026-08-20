using System.ComponentModel.DataAnnotations.Schema;

namespace MovieAPI.Models
{
    public class MovieDetail
    {
        public int Id { get; set; }
        public string Synopsis { get; set; } = "";
        public string Language { get; set; } = "";

        [Column(TypeName = "decimal(18,2)")]
        public decimal Budget { get; set; }

        // 1:1 med Movie
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
    }
}
