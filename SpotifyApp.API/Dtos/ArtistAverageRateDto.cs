namespace SpotifyApp.API.Dtos
{
    public class ArtistAverageRateDto
    {
        public ArtistDto Artist {get; set;}
        public double Rate {get; set;}

        public int NumberOfRates {get; set;}
    }
}