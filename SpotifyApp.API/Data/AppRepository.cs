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

        public async Task<PagedList<AlbumAverageRateDto>> GetAllAlbumsRate(RankingParams rankingParams)
        {
            var allAlbumsRates = this.context.Album.Include(x => x.Rates);

            var listFromSpotify = await GetAlbumsInfoFromSpotify(allAlbumsRates.Select(x => x.Id));         

            if(rankingParams.MinYear != 0 || rankingParams.MaxYear != DateTime.Today.Year) 
            {
                listFromSpotify = listFromSpotify
                .Where(x => int.Parse(x.Year) >= rankingParams.MinYear && int.Parse(x.Year) <= rankingParams.MaxYear)
                .ToList();
            }            

            var albumDictionary = listFromSpotify.ToDictionary(x => x.Id, x => x);

            var albumRanking = allAlbumsRates
                .Where(x => listFromSpotify.Select(y => y.Id).Contains(x.Id))
                .Select(album => new AlbumAverageRateDto()
            { 
                Album = this.mapper.Map<AlbumDto>(albumDictionary[album.Id]),
                Rate = Math.Round(album.Rates.Average(x => x.Rate), 3),
                NumberOfRates = album.Rates.Count
            });

            return await PagedList<AlbumAverageRateDto>.CreateAsync(albumRanking.OrderByDescending(x => x.Rate), rankingParams.PageNumber, rankingParams.PageSize);
        }

          public async Task<PagedList<ArtistAverageRateDto>> GetAllArtistsRate(RankingParams rankingParams)
        {
            var allArtistsRates = this.context.Artist.Include(x => x.Rates);

            var listFromSpotify = await GetArtistsInfoFromSpotify(allArtistsRates.Select(x => x.Id));         

               

            var artistDictionary = listFromSpotify.ToDictionary(x => x.Id, x => x);

            var artistRanking = allArtistsRates
                .Where(x => listFromSpotify.Select(y => y.Id).Contains(x.Id))
                .Select(artist => new ArtistAverageRateDto()
            { 
                Artist = this.mapper.Map<ArtistDto>(artistDictionary[artist.Id]),
                Rate = Math.Round(artist.Rates.Average(x => x.Rate), 3),
                NumberOfRates = artist.Rates.Count
            });

            return await PagedList<ArtistAverageRateDto>.CreateAsync(artistRanking.OrderByDescending(x => x.Rate), rankingParams.PageNumber, rankingParams.PageSize);
        }
       
        public async Task<PagedList<AlbumUserRateDto>> GetMyRates(RankingParams rankingParams, int userId)
        {
            var userRates = this.context.AlbumRates.Where(x => x.UserId == userId);
            var albumsId = userRates.Select(r => r.AlbumId);

            var listFromSpotify = await GetAlbumsInfoFromSpotify(albumsId);           

            var albumDictionary = listFromSpotify.ToDictionary(x => x.Id, x => x);
             var userRatesToReturn = userRates.Select(album => new AlbumUserRateDto()
            { 
                Album = this.mapper.Map<AlbumDto>(albumDictionary[album.AlbumId]),
                Rate = album.Rate,
                DateOfRate = album.RatedDate
            });

            return await PagedList<AlbumUserRateDto>.CreateAsync(userRatesToReturn.OrderByDescending(x => x.DateOfRate), rankingParams.PageNumber, rankingParams.PageSize);
        }

        public async Task<PagedList<ArtistUserRateDto>> GetMyArtistsRates(RankingParams rankingParams, int userId)
        {
            var userRates = this.context.ArtistRates.Where(x => x.UserId == userId);
            var artistsId = userRates.Select(r => r.ArtistId);

            var listFromSpotify = await GetArtistsInfoFromSpotify(artistsId);           

            var albumDictionary = listFromSpotify.ToDictionary(x => x.Id, x => x);
             var userRatesToReturn = userRates.Select(artist => new ArtistUserRateDto()
            { 
                Artist = this.mapper.Map<ArtistDto>(albumDictionary[artist.ArtistId]),
                Rate = artist.Rate,
                DateOfRate = artist.RatedDate
            });

            return await PagedList<ArtistUserRateDto>.CreateAsync(userRatesToReturn.OrderByDescending(x => x.DateOfRate), rankingParams.PageNumber, rankingParams.PageSize);
        }

        public async Task<int> GetAlbumRateForUser(string albumId, int userId)
        {
            var albumRate =  await this.context.AlbumRates.FirstOrDefaultAsync(x => x.AlbumId == albumId && x.UserId == userId);
            return albumRate?.Rate ?? 0;
        }

         public async Task<int> GetArtistRateForUser(string artistId, int userId)
        {
            var artistRate =  await this.context.ArtistRates.FirstOrDefaultAsync(x => x.ArtistId == artistId && x.UserId == userId);
            return artistRate?.Rate ?? 0;
        }

        public Task<User> GetUser(int id)
        {
            var user = this.context.Users.Include(a => a.AlbumRates).Include(p => p.Photo).FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await this.context.Users.Include(a => a.AlbumRates).Include(p => p.Photo).ToListAsync();

            return users;
        }

        public async Task<bool> SaveAll()
        {
            return await this.context.SaveChangesAsync() > 0;
        }

        public async Task<AlbumRate> GetAlbumRate(int userId, string albumId)
        {
            return await this.context.AlbumRates.FirstOrDefaultAsync(u => u.UserId == userId && u.AlbumId == albumId);
        }

         public async Task<ArtistRate> GetArtistRate(int userId, string artistId)
        {
            return await this.context.ArtistRates.FirstOrDefaultAsync(u => u.UserId == userId && u.ArtistId == artistId);
        }

         public async Task<Album> GetAlbum(string albumId)
        {
            return await this.context.Album.FirstOrDefaultAsync(u => u.Id == albumId);
        }      

        public async Task<Artist> GetArtist(string artistId)
        {
            return await this.context.Artist.FirstOrDefaultAsync(u => u.Id == artistId);
        }      

        public async void AddAlbum(Album album)
        {
            await this.context.Album.AddAsync(album);
        }

        public async void AddArtist(Artist artist)
        {
            await this.context.Artist.AddAsync(artist);
        }


         private async Task<List<AlbumDto>> GetAlbumsInfoFromSpotify(IQueryable<string> listOfAlbumsId)
        {
            var i = 0;
            var tempList = new List<string>();
            var listFromSpotify = new List<AlbumDto>();

            foreach(var a in listOfAlbumsId)
            {
                i++;
                tempList.Add(a);
                if (tempList.Count % 20 != 0 && i != listOfAlbumsId.Count())
                {
                    continue;
                }
                var albumsInfoDto = await this.spotifyData.GetSpotifyAlbums(tempList.ToList());
                listFromSpotify.AddRange(albumsInfoDto);
                tempList.Clear();
            }

            return listFromSpotify;
        }

           private async Task<List<ArtistDto>> GetArtistsInfoFromSpotify(IQueryable<string> listOfArtistsId)
        {
            var i = 0;
            var tempList = new List<string>();
            var listFromSpotify = new List<ArtistDto>();

            foreach(var a in listOfArtistsId)
            {
                i++;
                tempList.Add(a);
                if (tempList.Count % 20 != 0 && i != listOfArtistsId.Count())
                {
                    continue;
                }
                var artistsInfoDto = await this.spotifyData.GetSpotifyArtists(tempList.ToList());
                listFromSpotify.AddRange(artistsInfoDto);
                tempList.Clear();
            }

            return listFromSpotify;
        }

        public async Task<PagedList<Comment>> GetCommentsForAlbum(CommentParams commentParams)
        {
            var comments = this.context.Comments
            .Include(u => u.Commenter).ThenInclude(p => p.Photo)
            .AsQueryable()
            .Where(a => a.AlbumId == commentParams.AlbumId);

            comments = comments.OrderByDescending(d => d.CommentSent);

            return await PagedList<Comment>.CreateAsync(comments, commentParams.PageNumber, commentParams.PageSize);
        }

        public async Task<Comment> GetComment(int id)
        {
            return await this.context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}

