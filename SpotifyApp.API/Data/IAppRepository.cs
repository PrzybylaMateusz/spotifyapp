using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpotifyApp.API.Dtos;
using SpotifyApp.API.Helpers;
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
        Task<PagedList<AlbumAverageRateDto>> GetAllAlbumsRate(RankingParams rankingParams);
        Task<PagedList<ArtistAverageRateDto>> GetAllArtistsRate(RankingParams rankingParams);
        Task<PagedList<AlbumUserRateDto>> GetMyRates(RankingParams rankingParams, int userId);
        Task<PagedList<ArtistUserRateDto>> GetMyArtistsRates(RankingParams rankingParams, int userId);
        Task<double> GetSpecificAlbumAvaregeRate(string id);

        Task<int> GetAlbumRateForUser(string id, int userId);
        Task<int> GetArtistRateForUser(string id, int userId);

        Task<AlbumRate> GetAlbumRate(int userId, string albumId);
        Task<ArtistRate> GetArtistRate(int userId, string artistId);
        Task<Album> GetAlbum(string albumId);
        Task<Artist> GetArtist(string artistId);
        void AddAlbum(Album album);
        void AddArtist(Artist artist);

        Task<Comment> GetComment(int id);
        Task<PagedList<Comment>> GetCommentsForAlbum(CommentParams commentParams);
    }
}