using System;

namespace SpotifyApp.API.Models
{
    public class Album
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Artist { get; set; }

        public string Year { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public string CoverUrl { get; set; }
    }
}