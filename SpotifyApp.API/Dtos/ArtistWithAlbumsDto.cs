using System.Collections.Generic;

namespace SpotifyApp.API.Dtos
{
    public class ArtistWithAlbumsDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public List<AlbumDto> Albums{ get; set;}
    }
}