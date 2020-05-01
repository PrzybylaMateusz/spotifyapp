using System.Collections.Generic;
using System.Threading.Tasks;
using SpotifyApp.API.Dtos;

namespace SpotifyApp.API.Data
{
    public interface ISpotifyData
    {
        Task<IEnumerable<AlbumDto>> GetSpotifyAlbums(List<string> albumsIdToGet);
        Task<IEnumerable<ArtistDto>> GetSpotifyArtists(List<string> artistsIdToGet);
        Task<AlbumDto> GetSpotifyAlbum(string id);
        Task<ArtistDto> GetSpotifyArtist(string id);
        Task<IEnumerable<AlbumDto>> SearchSpotifyAlbums(string id);
        Task<IEnumerable<ArtistDto>> SearchSpotifyArtists(string id);
    }
}