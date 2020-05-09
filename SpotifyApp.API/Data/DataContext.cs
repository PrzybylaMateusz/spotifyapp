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
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Comment> Comments { get; set; }

         protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AlbumRate>().HasKey(k => new {k.UserId, k.AlbumId});

            builder.Entity<AlbumRate>().HasOne(a => a.Album)
            .WithMany(r => r.Rates)
            .HasForeignKey(a => a.AlbumId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AlbumRate>().HasOne(u => u.User)
            .WithMany(a => a.AlbumRates)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ArtistRate>().HasKey(k => new {k.UserId, k.ArtistId});

            builder.Entity<ArtistRate>().HasOne(a => a.Artist)
            .WithMany(r => r.Rates)
            .HasForeignKey(a => a.ArtistId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ArtistRate>().HasOne(u => u.User)
            .WithMany(a => a.ArtistRates)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comment>().HasOne(c => c.Commenter)
            .WithMany(c => c.Comments)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comment>().HasOne(a => a.Album)
            .WithMany(c => c.Comments)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }    
}