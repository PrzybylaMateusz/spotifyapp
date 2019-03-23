using Microsoft.EntityFrameworkCore;
using SpotifyApp.API.Models;

namespace SpotifyApp.API.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}

        public DbSet<Album> Albums { get; set; }
    }
}