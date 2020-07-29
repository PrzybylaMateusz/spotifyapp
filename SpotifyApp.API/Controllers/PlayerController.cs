using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;
using SpotifyApp.API.Dtos;

namespace SpotifyApp.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private static SpotifyWebAPI _spotify;

        [AllowAnonymous]
        [HttpGet("{tokenSpotify}")]
        public async Task<IActionResult> GetTrack(string tokenSpotify)
        {
             _spotify = new SpotifyWebAPI()
                {
                    AccessToken = tokenSpotify,
                    TokenType = "Bearer"
                };

            var context = await _spotify.GetPlayingTrackAsync();

            if(context.Item == null)
            {
                return Ok(null);
            }

            var currentAlbum = context.Item.Album;

            var album = new AlbumDto(){
                Artist = string.Join(",", context.Item.Artists[0].Name),
                Name = currentAlbum.Name,
                Id = currentAlbum.Id,
                UserId = 1,
                CoverUrl = currentAlbum.Images[0].Url,
                Year = currentAlbum.ReleaseDate.Substring(0, 4),
                ArtistId = context.Item.Artists[0].Id,            
            };

            var currentlyPlayed = new CurrentlyPlayedDto(){
                TrackName = context.Item.Name,
                Album = album
            };

            return Ok(currentlyPlayed);
        }
    }
}