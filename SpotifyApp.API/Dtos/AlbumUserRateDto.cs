using System;

namespace SpotifyApp.API.Dtos
{
    public class AlbumUserRateDto
    {
        public AlbumDto Album {get; set;}
        public double Rate {get; set;}

        public DateTime DateOfRate {get; set;}
    }
}