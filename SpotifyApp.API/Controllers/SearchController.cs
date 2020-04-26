using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;
using SpotifyApp.API.Data;
using SpotifyApp.API.Dtos;

namespace SpotifyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        // private readonly string _clientId = "69bbb47bc12a4a7cba51c70bc2ea6764";
        // private readonly string _secret = "a85e6ed0212a4c83b1326213d358720e";

        private readonly IMapper _mapper;
        private readonly ISpotifyData _spotifyData;
        public SearchController(IMapper mapper, ISpotifyData spotifyData)
        {
            _mapper = mapper;
            _spotifyData = spotifyData;
        }

         [AllowAnonymous]
        [HttpGet("artist/{id}")]
        // public async Task<IActionResult> GetSearchResults()
        public async Task<IActionResult> GetArtistSearch(string id)
        {
            var artists = await _spotifyData.SearchSpotifyArtists(id);
            var artistsToReturn = _mapper.Map<IEnumerable<ArtistDto>>(artists);
            return Ok(artists);
        }

        [AllowAnonymous]
        [HttpGet("album/{id}")]
        // public async Task<IActionResult> GetSearchResults()
        public async Task<IActionResult> GetAlbumSearch(string id)
        {
            var albums = await _spotifyData.SearchSpotifyAlbums(id);
            var albumsToReturn = _mapper.Map<IEnumerable<AlbumDto>>(albums);
            return Ok(albumsToReturn);
            // SearchItem item = await _spotify.SearchItemsAsync(id, SearchType.Album | SearchType.Playlist);

            // CredentialsAuth auth = new CredentialsAuth(_clientId, _secret);
            // Token token = await auth.GetToken();
            // _spotify = new SpotifyWebAPI()
            // {
            //     AccessToken = token.AccessToken,
            //     TokenType = token.TokenType
            // };

            // SearchItem item = _spotify.SearchItems(id, SearchType.Album | SearchType.Playlist);

            // ErrorResponse error = _spotify.SetVolume(50);

            // PlaybackContext context = _spotify.GetPlayback();
            // PlaybackContext context = new PlaybackContext();
            // Paging<FullTrack> tracks = _spotify.GetUsersTopTracks();
            // SimpleAlbum albumToReturn = new SimpleAlbum();
            // AuthorizationCodeAuth auth =
            //             new AuthorizationCodeAuth(_clientId, _secret, "http://localhost:4200", "http://localhost:4200", Scope.UserReadCurrentlyPlaying | Scope.UserReadPlaybackState);
            // auth.AuthReceived += AuthOnAuthReceived;

            // auth.Start(); // Starts an internal HTTP Server

            // auth.OpenBrowser();
            // Console.ReadLine();
            // auth.Stop(0);

            // // var track = GetCurrentTrack("https://api.spotify.com/v1/me/player/currently-playing", token.AccessToken);
            // Console.WriteLine("asdasdas");
            // return Ok(context);
        }
        // private static async void AuthOnAuthReceived(object sender, AuthorizationCode payload)
        // {
        //     AuthorizationCodeAuth auth = (AuthorizationCodeAuth)sender;
        //     auth.Stop();

        //     Console.WriteLine(payload.Error);

        //     Token token = await auth.ExchangeCode(payload.Code);
        //     SpotifyWebAPI api = new SpotifyWebAPI
        //     {
        //         AccessToken = token.AccessToken,
        //         TokenType = token.TokenType
        //     };

        //     PrintUsefulData(api);
        // }

        // private async static void PrintUsefulData(SpotifyWebAPI api)
        // {
        //     // var track = api.GetPlayingTrack();
        //     // Console.WriteLine("asdasd");
        //     // Console.WriteLine(track);
        //     PrivateProfile profile = await api.GetPrivateProfileAsync();
        //     var track = await api.GetPlayingTrackAsync();
        //     string name = string.IsNullOrEmpty(profile.DisplayName) ? profile.Id : profile.DisplayName;
        //     Console.WriteLine($"Hello there, {name}!");

        //     Console.WriteLine("Your actual track:");
        //     Console.WriteLine(track.Item);

        //     Console.WriteLine(track.Item.Name);
        //     // Paging<SimplePlaylist> playlists = await api.GetUserPlaylistsAsync(profile.Id);
        //     // do
        //     // {
        //     //     playlists.Items.ForEach(playlist =>
        //     //     {
        //     //         Console.WriteLine($"- {playlist.Name}");
        //     //     });
        //     //     playlists = await api.GetNextPageAsync(playlists);
        //     // } while (playlists.HasNextPage());

        // }
        // public string GetCurrentTrack(string uri, string token)
        // {
        //     HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
        //     request.Method = "GET";
        //     request.Headers.Add("Authorization", "Bearer " + token);
        //     request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

        //     using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //     using (Stream stream = response.GetResponseStream())
        //     using (StreamReader reader = new StreamReader(stream))
        //     {
        //         return reader.ReadToEnd();
        //     }
        // }
    }
}