using System;

namespace SpotifyApp.API.Models
{
    public class ArtistRate
    {
        public string ArtistId { get; set; }
        public int UserId { get; set; }
        public Artist Artist { get; set; }
        public User User  {get; set;}
        public int Rate { get; set; }
        public DateTime RatedDate { get; set; }
    }
}