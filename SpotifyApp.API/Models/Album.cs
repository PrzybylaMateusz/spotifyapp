using System.Collections.Generic;

namespace SpotifyApp.API.Models
{
    public class Album
    {
        public string Id { get; set; }
        public ICollection<AlbumRate> Rates { get; set; }
    }
}