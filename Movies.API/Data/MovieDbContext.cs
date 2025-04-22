using Microsoft.EntityFrameworkCore;
using Movies.Models;

namespace Movies.API.Data;

public class MovieDbContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<MovieReview> Reviews { get; set; }

    public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>()
            .Property(p => p.Genre).HasConversion<string>();
    }
}
