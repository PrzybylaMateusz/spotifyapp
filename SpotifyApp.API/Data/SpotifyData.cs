using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyApp.API.Dtos;

namespace SpotifyApp.API.Data
{
    public class SpotifyData : ISpotifyData
    {
        private const string ClientId = "69bbb47bc12a4a7cba51c70bc2ea6764";
        private const string SecretId = "a85e6ed0212a4c83b1326213d358720e";

        public async Task<IEnumerable<AlbumDto>> SearchSpotifyAlbums(string keyword)
        {
            var auth = new CredentialsAuth(ClientId, SecretId);
            var token = await auth.GetToken();
            var api = new SpotifyWebAPI()
            {
                TokenType = token.TokenType,
                AccessToken = token.AccessToken
            };

            var searchItem = await api.SearchItemsAsync(keyword, SearchType.Album);

            var albumsToReturn = new List<AlbumDto>();

            foreach (var album in searchItem.Albums.Items)
            {
                var albumToReturn = new AlbumDto
                {
                    Artist = album.Artists[0].Name,
                    Name = album.Name,
                    Id = album.Id,
                    UserId = 1,
                    CoverUrl = album.Images[0].Url,
                    Year = album.ReleaseDate.Substring(0, 4)
                };
                albumsToReturn.Add(albumToReturn);
            }

            return albumsToReturn;
        }

        public async Task<AlbumDto> GetSpotifyAlbum(string id)
        {
            var auth = new CredentialsAuth(ClientId, SecretId);
            var token = await auth.GetToken();
            var api = new SpotifyWebAPI()
            {
                TokenType = token.TokenType,
                AccessToken = token.AccessToken
            };
            
            var albumFromSpotify = await api.GetAlbumAsync(id);
            if (!albumFromSpotify.HasError())
                return new AlbumDto()
                {
                    Artist = string.Join(",", albumFromSpotify.Artists.Select((x) => x.Name)),
                    Name = albumFromSpotify.Name,
                    Id = albumFromSpotify.Id,
                    UserId = 1,
                    CoverUrl = albumFromSpotify.Images[0].Url,
                    Year = albumFromSpotify.ReleaseDate.Substring(0, 4),
                    ArtistId = albumFromSpotify.Artists[0].Id,
                };
            if (albumFromSpotify.Error.Message != "invalid id")
            {
                throw new Exception("Problem with retrieving album data from spotify");
            }

            throw new Exception($"Album with id: {id} was not found.");

        }

        public async Task<ArtistWithAlbumsDto> GetSpotifyArtist(string id)
        {
            var auth = new CredentialsAuth(ClientId, SecretId);
            var token = await auth.GetToken();
            var api = new SpotifyWebAPI()
            {
                TokenType = token.TokenType,
                AccessToken = token.AccessToken
            };

            var artistFromSpotify = await api.GetArtistAsync(id);

            var albumsForArtist = await api.GetArtistsAlbumsAsync(id);

            if (!albumsForArtist.HasError() || !artistFromSpotify.HasError())
            {
                var albumsToReturn = new List<AlbumDto>();
                foreach (var album in albumsForArtist.Items)
                {
                    var albumToReturn = new AlbumDto
                    {
                        Artist = artistFromSpotify.Name,
                        Name = album.Name,
                        Id = album.Id,
                        CoverUrl = album.Images[0].Url,
                        UserId = 1,
                        Year = album.ReleaseDate.Substring(0, 4)
                    };
                    albumsToReturn.Add(albumToReturn);
                }

                return new ArtistWithAlbumsDto()
                {
                    Name = artistFromSpotify.Name,
                    Id = artistFromSpotify.Id,
                    PhotoUrl = artistFromSpotify.Images[0].Url,
                    Albums = albumsToReturn
                };
            }
            if (artistFromSpotify.Error.Message != "invalid id")
            {
                throw new Exception("Problem with retrieving artist data from spotify");
            }

            throw new Exception($"Artist with id: {id} was not found.");

        }

        public async Task<IEnumerable<AlbumDto>> GetSpotifyAlbums(List<string> albumsIdToGet)
        {
            var auth = new CredentialsAuth(ClientId, SecretId);
            var token = await auth.GetToken();
            var api = new SpotifyWebAPI()
            {
                TokenType = token.TokenType,
                AccessToken = token.AccessToken
            };

            var albumsFromSpotify = await api.GetSeveralAlbumsAsync(albumsIdToGet);

            var albumsToReturn = new List<AlbumDto>();

            foreach (var album in albumsFromSpotify.Albums)
            {
                var albumToReturn = new AlbumDto
                {
                    Artist = album.Artists[0].Name,
                    ArtistId = album.Artists[0].Id,
                    Name = album.Name,
                    Id = album.Id,
                    UserId = 1,
                    CoverUrl = album.Images[0].Url,
                    Year = album.ReleaseDate.Substring(0, 4)
                };
                albumsToReturn.Add(albumToReturn);
            }

            return albumsToReturn;
        }

         public async Task<IEnumerable<ArtistDto>> GetSpotifyArtists(List<string> artistsIdToGet)
        {
            var auth = new CredentialsAuth(ClientId, SecretId);
            var token = await auth.GetToken();
            var api = new SpotifyWebAPI()
            {
                TokenType = token.TokenType,
                AccessToken = token.AccessToken
            };

            var artistsFromSpotify = await api.GetSeveralArtistsAsync(artistsIdToGet);

            var artistsToReturn = new List<ArtistDto>();

            foreach (var artist in artistsFromSpotify.Artists)
            {
                var artistToReturn = new ArtistDto
                {
                    Name = artist.Name, Id = artist.Id, PhotoUrl = artist.Images[0].Url
                };
                artistsToReturn.Add(artistToReturn);
            }

            return artistsToReturn;
        }

         public async Task<IEnumerable<ArtistDto>> SearchSpotifyArtists(string keyword)
        {
            var auth = new CredentialsAuth(ClientId, SecretId);
            var token = await auth.GetToken();
            var api = new SpotifyWebAPI()
            {
                TokenType = token.TokenType,
                AccessToken = token.AccessToken
            };

            var searchItem = await api.SearchItemsAsync(keyword, SearchType.Artist);

            var artistsToReturn = new List<ArtistDto>();

            foreach (var artist in searchItem.Artists.Items)
            {
                var artistToReturn = new ArtistDto
                {
                    Name = artist.Name,
                    Id = artist.Id,
                    PhotoUrl = artist.Images.Count > 0 ? artist.Images[0].Url : null
                };
                artistsToReturn.Add(artistToReturn);
            }

            return artistsToReturn;
        }
    }
}