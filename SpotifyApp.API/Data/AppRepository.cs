using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;
using SpotifyApp.API.Dtos;
using SpotifyApp.API.Helpers;
using SpotifyApp.API.Models;

namespace SpotifyApp.API.Data
{
    public class AppRepository : IAppRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        private readonly ISpotifyData spotifyData;

        public AppRepository(DataContext context, IMapper mapper, ISpotifyData spotifyData)
        {
            this.context = context;
            this.mapper = mapper;
            this.spotifyData = spotifyData;
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

        public async Task<PagedList<AlbumOverallRateDto>> GetAllAlbumsRate(RankingParams rankingParams)
        {
            var allAlbumsRates = this.context.AlbumsRates;
            var albumDistincted = allAlbumsRates.Select(x => x.Album).Distinct();

            var i = 0;
            var tempList = new List<string>();
            var listFromSpotify = new List<Album>();

            foreach(var a in albumDistincted)
            {
                i++;
                tempList.Add(a);
                if(tempList.Count() % 20==0 || i == albumDistincted.Count()){
                    var albumsInfoDto = await this.spotifyData.GetSpotifyAlbums(tempList.ToList());
                    listFromSpotify.AddRange(albumsInfoDto);
                    tempList.Clear();
                }
            }           

            Dictionary<string, Album> albumDictionary = listFromSpotify.ToDictionary(x => x.Id, x => x);           

            var albumRanking = albumDistincted.Select(album => new AlbumOverallRateDto()
            { 
                Album = this.mapper.Map<AlbumDto>(albumDictionary[album]),
                Rate = Math.Round(allAlbumsRates.Where(x=> x.Album == album).Average(x => x.Rate), 3) 
            });

            return await PagedList<AlbumOverallRateDto>.CreateAsync(albumRanking.OrderByDescending(x => x.Rate), rankingParams.PageNumber, rankingParams.PageSize);
        }

        public async Task<int> GetAlbumRateForUser(string albumId, int userId)
        {
            var albumRate =  await this.context.AlbumsRates.FirstOrDefaultAsync(x => x.Album == albumId && x.UserId == userId);
            if(albumRate == default)
            {
                return 0;
            }
            return albumRate.Rate;
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
            if(await this.context.AlbumsRates.AnyAsync(x => x.Album == albumRate.Album && x.UserId == albumRate.UserId))
            {
                var actualAlbumRate = await this.context.AlbumsRates.FirstOrDefaultAsync(x => x.Album == albumRate.Album && x.UserId == albumRate.UserId);

                // this.context.Entry(actualAlbumRate).CurrentValues.SetValues(albumRate);

                actualAlbumRate.Rate = albumRate.Rate;
                actualAlbumRate.RatedDate = albumRate.RatedDate;

                // this.mapper.Map(albumRate, actualAlbumRate);
                // actualAlbumRate = albumRate;
                await this.context.SaveChangesAsync();
            }
            else{
                await this.context.AlbumsRates.AddAsync(albumRate);
                await this.context.SaveChangesAsync();
            }           
        }
    }
}