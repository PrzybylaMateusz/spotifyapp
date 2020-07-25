namespace SpotifyApp.API.Dtos
{
    public class CurrentlyPlayedDto
    {
        public string TrackName { get; set; }
        public AlbumDto Album { get; set; }
    }
}