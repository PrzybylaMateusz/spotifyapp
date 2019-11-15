using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Models;
using SpotifyApp.API.Models;

namespace SpotifyApp.API.Data
{
    public class SpotifyData : ISpotifyData
    {
        private readonly string _clientId = "69bbb47bc12a4a7cba51c70bc2ea6764";
        private readonly string _secretId = "a85e6ed0212a4c83b1326213d358720e";

        public async Task<IEnumerable<Album>> GetSpotifyAlbums()
        {
            var listOfAlbums = new List<string>();
            listOfAlbums.Add("6YNIEeDWqC09YIWzhoSVLg");
            listOfAlbums.Add("6RgX6mXKrsJYgGpX5K2JQJ");
            listOfAlbums.Add("5zi7WsKlIiUXv09tbGLKsE");
            listOfAlbums.Add("2T64N96AVfsrRFJCUXQEoZ");
            listOfAlbums.Add("79dL7FLiJFOO0EoehUHQBv");

            CredentialsAuth auth = new CredentialsAuth(_clientId, _secretId);
            Token token = await auth.GetToken();
            SpotifyWebAPI api = new SpotifyWebAPI()
            {
                TokenType = token.TokenType,
                AccessToken = token.AccessToken
            };

            SeveralAlbums albumsFromSpotify = await api.GetSeveralAlbumsAsync(listOfAlbums);

            var albumsToReturn = new List<Album>();

            Console.Write(albumsFromSpotify.Albums[0]);

            foreach (var album in albumsFromSpotify.Albums)
            {
                var albumToReturn = new Album();
                albumToReturn.Artist = album.Artists[0].Name;
                albumToReturn.Name = album.Name;
                albumToReturn.Id = Guid.NewGuid();
                albumToReturn.UserId = 1;
                albumToReturn.CoverUrl = album.Images[0].Url;
                albumsToReturn.Add(albumToReturn);
            }

            return albumsToReturn;
        }
    }
}