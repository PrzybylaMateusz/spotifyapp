using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpotifyApp.API.Models;

namespace SpotifyApp.API.Data
{
    public interface IAppRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<IEnumerable<Album>> GetAlbums();
        Task<Album> GetAlbum(Guid id);
        Task RateAlbum(AlbumRate albumRate);
    }
}