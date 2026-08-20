using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;

namespace MovieAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        // public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<Movie> Movies => Set<Movie>();
        //public DbSet<MovieDetail> MovieDetails { get; set; } = null!;
        public DbSet<MovieDetail> MovieDetails => Set<MovieDetail>();

        // public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Review> Reviews => Set<Review>();

        // public DbSet<Actor> Actors { get; set; } = null!;
        public DbSet<Actor> Actors => Set<Actor>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Här kan du lägga till konfiguration för dina entiteter, t.ex.:
            // 1:1
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.MovieDetail)
                .WithOne(md => md.Movie)
                .HasForeignKey<MovieDetail>(md => md.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            // N:M Movie – Actor via MovieActor
            modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });
        }
    }
}
