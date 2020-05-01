using System;
using System.Collections.Generic;

namespace SpotifyApp.API.Dtos
{
    public class AlbumDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string ArtistId { get; set; }
        public int UserId { get; set; }
        public string CoverUrl { get; set; }
        public string Year { get; set; }
    }
}