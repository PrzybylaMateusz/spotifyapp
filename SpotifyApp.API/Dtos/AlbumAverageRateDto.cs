namespace SpotifyApp.API.Dtos
{
    public class AlbumAverageRateDto
    {
        public AlbumDto Album {get; set;}
        public double Rate {get; set;}

        public int NumberOfRates {get; set;}
    }
}