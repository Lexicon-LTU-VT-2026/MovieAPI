using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MovieAPI.Data;
using MovieAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace MovieAPI.Extensions
{
    public static class SeedDataExtensions
    {
        public static void SeedData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Se till att databasen är skapad (om du inte kör migrationer manuellt)
            // context.Database.EnsureCreated(); // oftast vill du INTE använda detta i produktion

            if (context.Movies.Any())
            {
                return; // redan seedat
            }

            // Actors
            var actor1 = new Actor { Name = "Tom Hanks", BirthYear = 1956 };
            var actor2 = new Actor { Name = "Meryl Streep", BirthYear = 1949 };
            var actor3 = new Actor { Name = "Leonardo DiCaprio", BirthYear = 1974 };

            context.Actors.AddRange(actor1, actor2, actor3);

            // Movies + MovieDetails
            var movie1 = new Movie
            {
                Title = "Forrest Gump",
                Year = 1994,
                Genre = "Drama",
                Duration = 142,
                MovieDetail = new MovieDetail
                {
                    Synopsis = "En enkel man med gott hjärta lever ett ovanligt liv.",
                    Language = "Engelska",
                    Budget = 55_000_000m
                },
                Reviews = new List<Review>
            {
                new Review { ReviewerName = "Alice", Comment = "En klassiker!", Rating = 5 },
                new Review { ReviewerName = "Bob", Comment = "Mycket bra skådespel.", Rating = 4 }
            },
                MovieActors = new List<MovieActor>
            {
                new MovieActor { Actor = actor1 }
            }
            };

            var movie2 = new Movie
            {
                Title = "The Devil Wears Prada",
                Year = 2006,
                Genre = "Komedi/Drama",
                Duration = 109,
                MovieDetail = new MovieDetail
                {
                    Synopsis = "En ung kvinna får jobba som assistent åt en krävande modeeditor.",
                    Language = "Engelska",
                    Budget = 35_000_000m
                },
                Reviews = new List<Review>
            {
                new Review { ReviewerName = "Charlie", Comment = "Rolig och välgjord.", Rating = 4 }
            },
                MovieActors = new List<MovieActor>
            {
                new MovieActor { Actor = actor2 }
            }
            };

            var movie3 = new Movie
            {
                Title = "Inception",
                Year = 2010,
                Genre = "Sci-Fi/Action",
                Duration = 148,
                MovieDetail = new MovieDetail
                {
                    Synopsis = "En tjuv som stjäl hemligheter via drömmar får ett sista uppdrag.",
                    Language = "Engelska",
                    Budget = 160_000_000m
                },
                Reviews = new List<Review>
            {
                new Review { ReviewerName = "Dana", Comment = "Mycket smart och visuellt imponerande.", Rating = 5 },
                new Review { ReviewerName = "Erik", Comment = "Lite rörig men spännande.", Rating = 4 }
            },
                MovieActors = new List<MovieActor>
            {
                new MovieActor { Actor = actor3 }
            }
            };

            context.Movies.AddRange(movie1, movie2, movie3);

            context.SaveChanges();
        }
    }
}
