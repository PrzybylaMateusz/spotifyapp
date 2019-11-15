using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;

namespace SpotifyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly string _clientId = "69bbb47bc12a4a7cba51c70bc2ea6764";
        private readonly string _secret = "a85e6ed0212a4c83b1326213d358720e";

        private static string track = "";

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetTrack()
        {
            AuthorizationCodeAuth auth =
                       new AuthorizationCodeAuth(_clientId, _secret, "http://localhost:4200", "http://localhost:4200", Scope.UserReadCurrentlyPlaying | Scope.UserReadPlaybackState);
            auth.AuthReceived += AuthOnAuthReceived;
            auth.Start(); // Starts an internal HTTP Server

            auth.OpenBrowser();
            Console.ReadLine();
            auth.Stop(0);

            return Ok(track);
        }

        private static async void AuthOnAuthReceived(object sender, AuthorizationCode payload)
        {
            AuthorizationCodeAuth auth = (AuthorizationCodeAuth)sender;
            auth.Stop();

            Token token = await auth.ExchangeCode(payload.Code);
            SpotifyWebAPI api = new SpotifyWebAPI
            {
                AccessToken = token.AccessToken,
                TokenType = token.TokenType
            };

            GetCurrentlyPlayingTrack(api);
        }

        private async static void GetCurrentlyPlayingTrack(SpotifyWebAPI api)
        {
            var player = await api.GetPlayingTrackAsync();

            Console.WriteLine("Your actual track:");
            Console.WriteLine(player.Item);

            Console.WriteLine(player.Item.Name);

            track = player.Item.Name;
        }
    }
}