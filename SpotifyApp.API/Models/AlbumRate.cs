using System;

namespace SpotifyApp.API.Models
{
    public class AlbumRate
    {
        public int Id { get; set; }
        public string Album { get; set; }
        public User User { get; set; }
        public int Rate { get; set; }
        public DateTime RatedDate { get; set; }
    }
}