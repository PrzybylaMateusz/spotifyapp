using System;

namespace SpotifyApp.API.Models
{
    public class AlbumRate
    {
        public string AlbumId { get; set; }
        public int UserId { get; set; }
        public Album Album { get; set; }
        public User User  {get; set;}
        public int Rate { get; set; }
        public DateTime RatedDate { get; set; }
    }
}