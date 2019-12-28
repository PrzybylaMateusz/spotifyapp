using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpotifyApp.API.Models;

namespace SpotifyApp.API.Data
{
    public class AppRepository : IAppRepository
    {
        private readonly DataContext context;
        public AppRepository(DataContext context)
        {
            this.context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            context.Remove(entity);
        }

        public async Task<double> GetSpecificAlbumAvaregeRate(string id)
        {
            var albumRate = await this.context.AlbumsRates.Where(x => x.Album == id).AverageAsync(x => x.Rate);

            return albumRate;
        }

        public async Task<IEnumerable<AlbumRate>> GetAllRatesForSpecificAlbum(string id)
        {
            var albumRates = await this.context.AlbumsRates.Where(x => x.Album == id).ToListAsync();

            return albumRates;
        }

        public async Task<IEnumerable<AlbumRate>> GetAllAlbumsRate()
        {
            var allAlbumsRates = await this.context.AlbumsRates.ToListAsync();

            return allAlbumsRates;
        }

        
        public async Task<IEnumerable<AlbumRate>> GetUniqueRatedAlbums()
        {
            var uniqueRatedAlbums = await this.context.AlbumsRates.GroupBy(x => x.Album).Select(x => x.First()).ToListAsync();

            return uniqueRatedAlbums;
        }

        public Task<User> GetUser(int id)
        {
            var user = this.context.Users.Include(a => a.AlbumsRates).Include(p => p.Photo).FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await this.context.Users.Include(a => a.AlbumsRates).Include(p => p.Photo).ToListAsync();

            return users;
        }

        public async Task<bool> SaveAll()
        {
            return await this.context.SaveChangesAsync() > 0;
        }

        public async Task RateAlbum(AlbumRate albumRate)
        {
            await this.context.AlbumsRates.AddAsync(albumRate);
            await this.context.SaveChangesAsync();
        }
    }
}