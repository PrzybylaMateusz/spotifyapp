using Microsoft.EntityFrameworkCore;
using SpotifyApp.API.Models;

namespace SpotifyApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }

        public DbSet<AlbumRate> AlbumRates { get; set; }
        public DbSet<ArtistRate> ArtistRates { get; set; }
        public DbSet<Album> Album { get; set; }

         protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AlbumRate>().HasKey(k => new {k.UserId, k.AlbumId});

            builder.Entity<AlbumRate>().HasOne(u => u.Album)
            .WithMany(u => u.Rates)
            .HasForeignKey(u => u.AlbumId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AlbumRate>().HasOne(u => u.User)
            .WithMany(u => u.AlbumRates)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ArtistRate>().HasKey(k => new {k.UserId, k.ArtistId});

            builder.Entity<ArtistRate>().HasOne(u => u.Artist)
            .WithMany(u => u.Rates)
            .HasForeignKey(u => u.ArtistId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ArtistRate>().HasOne(u => u.User)
            .WithMany(u => u.ArtistRates)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }    
}