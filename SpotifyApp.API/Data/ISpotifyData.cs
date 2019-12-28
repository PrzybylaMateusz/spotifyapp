using System.Collections.Generic;
using System.Threading.Tasks;
using SpotifyApp.API.Models;

namespace SpotifyApp.API.Data
{
    public interface ISpotifyData
    {
        Task<IEnumerable<Album>> GetSpotifyAlbums(List<string> albumsIdToGet);
        Task<IEnumerable<Album>> SearchSpotifyAlbums(string id);
    }
}