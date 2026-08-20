using System.ComponentModel.DataAnnotations;

namespace MovieAPI.DTOs
{
    public class MovieUpdateDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Title { get; set; } = "";

        [Required]
        [Range(1800, 2100)]
        public int Year { get; set; }

        [Required]
        [StringLength(50)]
        public string Genre { get; set; } = "";

        [Required]
        [Range(1, 600)]
        public int Duration { get; set; }

        // MovieDetails
        [Required]
        [StringLength(1000)]
        public string Synopsis { get; set; } = "";

        [Required]
        [StringLength(50)]
        public string Language { get; set; } = "";

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Budget { get; set; }
    }
}
