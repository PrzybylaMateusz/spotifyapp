using System;

namespace SpotifyApp.API.Dtos
{
    public class ArtistRateDto
    {
        public string ArtistId { get; set; }
        public int UserId { get; set; }
        public int Rate { get; set; }
        public DateTime RatedDate { get; set; }
    }
}