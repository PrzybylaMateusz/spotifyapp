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
                albumToReturn.Id = Guid.NewGuid();
                albumToReturn.UserId = 1;
                albumToReturn.CoverUrl = album.Images[0].Url;
                albumToReturn.Year = album.ReleaseDate.Substring(0, 4);
                albumsToReturn.Add(albumToReturn);
            }

            return albumsToReturn;
        }

        public async Task<IEnumerable<Album>> GetSpotifyAlbums()
        {
            var listOfAlbums = new List<string>();
            listOfAlbums.Add("1F8y2bg9V9nRoy8zuxo3Jt");
            listOfAlbums.Add("2Dnli6R27dyVX1GBLMudpN");
            listOfAlbums.Add("6b1HPtDuYioXwmw5xLLFQ9");
            listOfAlbums.Add("2T64N96AVfsrRFJCUXQEoZ");
            listOfAlbums.Add("79dL7FLiJFOO0EoehUHQBv");
            listOfAlbums.Add("1ZFjvEN3C2J1Q1xVhu2YaC");

            CredentialsAuth auth = new CredentialsAuth(_clientId, _secretId);
            Token token = await auth.GetToken();
            SpotifyWebAPI api = new SpotifyWebAPI()
            {
                TokenType = token.TokenType,
                AccessToken = token.AccessToken
            };

            SeveralAlbums albumsFromSpotify = await api.GetSeveralAlbumsAsync(listOfAlbums);

            var albumsToReturn = new List<Album>();

            foreach (var album in albumsFromSpotify.Albums)
            {
                var albumToReturn = new Album();
                albumToReturn.Artist = album.Artists[0].Name;
                albumToReturn.Name = album.Name;
                albumToReturn.Id = Guid.NewGuid();
                albumToReturn.UserId = 1;
                albumToReturn.CoverUrl = album.Images[0].Url;
                albumToReturn.Year = album.ReleaseDate.Substring(0, 4);
                albumsToReturn.Add(albumToReturn);
            }

            return albumsToReturn;
        }
    }
}