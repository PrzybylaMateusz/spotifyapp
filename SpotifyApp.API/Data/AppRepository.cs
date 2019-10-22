using System;
using System.Collections.Generic;
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

        public Task<Album> GetAlbum(Guid id)
        {
            var album = this.context.Albums.FirstOrDefaultAsync(u => u.Id == id);

            return album;
        }

        public async Task<IEnumerable<Album>> GetAlbums()
        {
            var albums = await this.context.Albums.ToListAsync();

            return albums;
        }

        public Task<User> GetUser(int id)
        {
            var user = this.context.Users.Include(p => p.Albums).FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await this.context.Users.Include(p => p.Albums).ToListAsync();

            return users;
        }

        public async Task<bool> SaveAll()
        {
            return await this.context.SaveChangesAsync() > 0;
        }
    }
}