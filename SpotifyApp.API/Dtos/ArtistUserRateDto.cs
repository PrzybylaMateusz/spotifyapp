using System;

namespace SpotifyApp.API.Dtos
{
    public class ArtistUserRateDto
    {
        public ArtistDto Artist {get; set;}
        public double Rate {get; set;}
        public DateTime DateOfRate {get; set;}
    }
}