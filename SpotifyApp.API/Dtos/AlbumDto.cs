using System;

namespace SpotifyApp.API.Dtos
{
    public class AlbumDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public int UserId { get; set; }
    }
}