using System.Collections.Generic;

namespace SpotifyApp.API.Models
{
    public class Artist
    {
        
        public string Id { get; set; }
        public ICollection<ArtistRate> Rates { get; set; }
    }
}