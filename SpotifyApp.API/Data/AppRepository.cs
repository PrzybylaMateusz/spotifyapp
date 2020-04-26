using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
            var albumRate = await this.context.AlbumsRates.Where(x => x.AlbumId == id).AverageAsync(x => x.Rate);

            return albumRate;
        }

        public async Task<IEnumerable<AlbumRate>> GetAllRatesForSpecificAlbum(string id)
        {
            var albumRates = await this.context.AlbumsRates.Where(x => x.AlbumId == id).ToListAsync();

            return albumRates;
        }

        public async Task<PagedList<AlbumAverageRateDto>> GetAllAlbumsRate(RankingParams rankingParams)
        {
            var allAlbumsRates = this.context.Album.Include(x => x.Rates);

            var i = 0;
            var tempList = new List<string>();
            var listFromSpotify = new List<AlbumDto>();

            foreach(var a in allAlbumsRates)
            {
                i++;
                tempList.Add(a.Id);
                if(tempList.Count() % 20==0 || i == allAlbumsRates.Count()){
                    var albumsInfoDto = await this.spotifyData.GetSpotifyAlbums(tempList.ToList());
                    listFromSpotify.AddRange(albumsInfoDto);
                    tempList.Clear();
                }
            }

            if(rankingParams.MinYear != 0 || rankingParams.MaxYear != DateTime.Today.Year) 
            {
                listFromSpotify = listFromSpotify
                .Where(x => int.Parse(x.Year) >= rankingParams.MinYear && int.Parse(x.Year) <= rankingParams.MaxYear)
                .ToList();
            }            

            Dictionary<string, AlbumDto> albumDictionary = listFromSpotify.ToDictionary(x => x.Id, x => x);

            var albumRanking = allAlbumsRates
                .Where(x => listFromSpotify.Select(y => y.Id).Contains(x.Id))
                .Select(album => new AlbumAverageRateDto()
            { 
                Album = this.mapper.Map<AlbumDto>(albumDictionary[album.Id]),
                Rate = Math.Round(album.Rates.Average(x => x.Rate), 3),
                NumberOfRates = album.Rates.Count()
            });

            return await PagedList<AlbumAverageRateDto>.CreateAsync(albumRanking.OrderByDescending(x => x.Rate), rankingParams.PageNumber, rankingParams.PageSize);
        }

        public async Task<PagedList<AlbumUserRateDto>> GetMyRates(RankingParams rankingParams, int userId)
        {
            var userWithRates = await this.context.Users.Include(x => x.Rates).FirstOrDefaultAsync(u => u.Id == userId);
           
            var albumsId = userWithRates.Rates.Select(r => r.AlbumId);

            var i = 0;
            var tempList = new List<string>();
            var listFromSpotify = new List<AlbumDto>();

            foreach(var a in albumsId)
            {
                i++;
                tempList.Add(a);
                if(tempList.Count() % 20==0 || i == albumsId.Count()){
                    var albumsInfoDto = await this.spotifyData.GetSpotifyAlbums(tempList.ToList());
                    listFromSpotify.AddRange(albumsInfoDto);
                    tempList.Clear();
                }
            }      

            Dictionary<string, AlbumDto> albumDictionary = listFromSpotify.ToDictionary(x => x.Id, x => x);    
            Dictionary<string, RateforDict> userWithRatesDict = userWithRates.Rates
            .ToDictionary(x => x.AlbumId, x => new RateforDict{Rate = x.Rate, DateOfRate= x.RatedDate});    

            var albums = this.context.Album.Where(x => albumsId.Contains(x.Id));

            var albumRanking = albums.Select(album => new AlbumUserRateDto()
            { 
                Album = this.mapper.Map<AlbumDto>(albumDictionary[album.Id]),
                Rate = userWithRatesDict[album.Id].Rate,
                DateOfRate = userWithRatesDict[album.Id].DateOfRate,
            });

            return await PagedList<AlbumUserRateDto>.CreateAsync(albumRanking, rankingParams.PageNumber, rankingParams.PageSize);
        }

        public async Task<int> GetAlbumRateForUser(string albumId, int userId)
        {
            var albumRate =  await this.context.AlbumsRates.FirstOrDefaultAsync(x => x.AlbumId == albumId && x.UserId == userId);
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
            var user = this.context.Users.Include(a => a.Rates).Include(p => p.Photo).FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await this.context.Users.Include(a => a.Rates).Include(p => p.Photo).ToListAsync();

            return users;
        }

        public async Task<bool> SaveAll()
        {
            return await this.context.SaveChangesAsync() > 0;
        }

        public async Task<AlbumRate> GetRate(int userId, string albumId)
        {
            return await this.context.AlbumsRates.FirstOrDefaultAsync(u => u.UserId == userId && u.AlbumId == albumId);
        }

         public async Task<Album> GetAlbum(string albumId)
        {
            return await this.context.Album.FirstOrDefaultAsync(u => u.Id == albumId);
        }      

            public async void AddAlbum(Album album)
        {
            await this.context.Album.AddAsync(album);
        }
    }

    class RateforDict 
    {
        public int Rate { get; set; }
        public DateTime DateOfRate { get; set; }
    }
}

