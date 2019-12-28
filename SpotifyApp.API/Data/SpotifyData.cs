using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;
using SpotifyApp.API.Models;

namespace SpotifyApp.API.Data
{
    public class SpotifyData : ISpotifyData
    {
        private readonly string _clientId = "69bbb47bc12a4a7cba51c70bc2ea6764";
        private readonly string _secretId = "a85e6ed0212a4c83b1326213d358720e";

        public async Task<IEnumerable<Album>> SearchSpotifyAlbums(string keyword)
        {
            CredentialsAuth auth = new CredentialsAuth(_clientId, _secretId);
            Token token = await auth.GetToken();
            SpotifyWebAPI api = new SpotifyWebAPI()
            {
                TokenType = token.TokenType,
                AccessToken = token.AccessToken
            };

            SearchItem searchItem = await api.SearchItemsAsync(keyword, SearchType.Album);

            var albumsToReturn = new List<Album>();

            foreach (var album in searchItem.Albums.Items)
            {
                var albumToReturn = new Album();
                albumToReturn.Artist = album.Artists[0].Name;
                albumToReturn.Name = album.Name;
                albumToReturn.Id = album.Id;
                albumToReturn.UserId = 1;
                albumToReturn.CoverUrl = album.Images[0].Url;
                albumToReturn.Year = album.ReleaseDate.Substring(0, 4);
                albumsToReturn.Add(albumToReturn);
            }

            return albumsToReturn;
        }

        public async Task<IEnumerable<Album>> GetSpotifyAlbums(List<string> albumsIdToGet)
        {
            CredentialsAuth auth = new CredentialsAuth(_clientId, _secretId);
            Token token = await auth.GetToken();
            SpotifyWebAPI api = new SpotifyWebAPI()
            {
                TokenType = token.TokenType,
                AccessToken = token.AccessToken
            };

            SeveralAlbums albumsFromSpotify = await api.GetSeveralAlbumsAsync(albumsIdToGet);

            var albumsToReturn = new List<Album>();

            foreach (var album in albumsFromSpotify.Albums)
            {
                var albumToReturn = new Album();
                albumToReturn.Artist = album.Artists[0].Name;
                albumToReturn.Name = album.Name;
                albumToReturn.Id = album.Id;
                albumToReturn.UserId = 1;
                albumToReturn.CoverUrl = album.Images[0].Url;
                albumToReturn.Year = album.ReleaseDate.Substring(0, 4);
                albumsToReturn.Add(albumToReturn);
            }

            return albumsToReturn;
        }
    }
}