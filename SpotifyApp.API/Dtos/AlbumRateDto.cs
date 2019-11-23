using System;

namespace SpotifyApp.API.Dtos
{
    public class AlbumRateDto
    {
        public int Id { get; set; }
        public Guid Album { get; set; }
        public int UserId { get; set; }
        public int Rate { get; set; }
        public DateTime RatedDate { get; set; }
    }
}