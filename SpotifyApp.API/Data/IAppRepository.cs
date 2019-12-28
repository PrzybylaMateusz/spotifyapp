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
        Task<IEnumerable<AlbumRate>> GetAllAlbumsRate();
        Task<double> GetSpecificAlbumAvaregeRate(string id);
        Task RateAlbum(AlbumRate albumRate);
    }
}