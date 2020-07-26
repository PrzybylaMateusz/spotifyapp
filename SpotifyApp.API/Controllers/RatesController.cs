using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyApp.API.Data;
using SpotifyApp.API.Dtos;
using SpotifyApp.API.Models;
using SpotifyApp.API.Helpers;
using System.Security.Claims;

namespace SpotifyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController: ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IAppRepository appRepository;

        public RatesController(IMapper mapper, IAppRepository appRepository)
        {
            this.mapper = mapper;
            this.appRepository = appRepository;
        }

        [HttpPost("album")]
        public async Task<IActionResult> RateAlbum(AlbumRateDto albumRateDto)
        {
            if (albumRateDto.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var rate = await appRepository.GetAlbumRate(albumRateDto.UserId, albumRateDto.AlbumId);
            
            if(rate != null)
            {
                rate.Rate = albumRateDto.Rate;
                rate.RatedDate = albumRateDto.RatedDate;
                if(await appRepository.SaveAll())
                {
                    return Ok();
                }
                return BadRequest("Failed to rate album");
            }

            if(await appRepository.GetAlbum(albumRateDto.AlbumId) == null)
            {
                appRepository.AddAlbum(new Album {Id = albumRateDto.AlbumId});
            }

            rate = mapper.Map<AlbumRate>(albumRateDto);

            appRepository.Add(rate);
            if(await appRepository.SaveAll())
            {
                return Ok();
            }
            return BadRequest("Failed to rate album");
        }
        
        [HttpPost("artist")]
        public async Task<IActionResult> RateArtist(ArtistRateDto artistRateDto)
        {
            if (artistRateDto.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var rate = await appRepository.GetArtistRate(artistRateDto.UserId, artistRateDto.ArtistId);
            
            if(rate != null)
            {
                rate.Rate = artistRateDto.Rate;
                rate.RatedDate = artistRateDto.RatedDate;
                if(await appRepository.SaveAll())
                {
                    return Ok();
                }
                return BadRequest("Failed to rate artist");
            }

            if(await appRepository.GetArtist(artistRateDto.ArtistId) == null)
            {
                appRepository.AddArtist(new Artist {Id = artistRateDto.ArtistId});
            }

            rate = mapper.Map<ArtistRate>(artistRateDto);

            appRepository.Add(rate);
            if(await appRepository.SaveAll())
            {
                return Ok();
            }
            return BadRequest("Failed to rate artist");
        }

        [AllowAnonymous]
        [HttpGet("{albumId}/{userId}")]
        public async Task<IActionResult> GetAlbumRateForUser(string albumId, int userId)
        {
            var rate = await appRepository.GetAlbumRateForUser(albumId, userId);
            return Ok(rate);
        }

        [AllowAnonymous]
        [HttpGet("artist/{artistId}/{userId}")]
        public async Task<IActionResult> GetArtistRateForUser(string artistId, int userId)
        {
            var rate = await appRepository.GetArtistRateForUser(artistId, userId);
            return Ok(rate);
        }


        [AllowAnonymous]
        [HttpGet("albumranking")]
        public async Task<IActionResult> GetAlbumRanking([FromQuery]RankingParams rankingParams)
        {
            var allRates = await appRepository.GetAllAlbumsRate(rankingParams);   
            
            Response.AddPagination(allRates.CurrentPage, allRates.PageSize, allRates.TotalCount, allRates.TotalPages);

            return Ok(allRates);
        }

        [AllowAnonymous]
        [HttpGet("artistranking")]
        public async Task<IActionResult> GetArtistRanking([FromQuery]RankingParams rankingParams)
        {
            var allRates = await appRepository.GetAllArtistsRate(rankingParams);   
            
            Response.AddPagination(allRates.CurrentPage, allRates.PageSize, allRates.TotalCount, allRates.TotalPages);

            return Ok(allRates);
        }

        [AllowAnonymous]
        [HttpGet("myrates")]
        public async Task<IActionResult> GetMyRates([FromQuery]RankingParams rankingParams)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var myRates = await appRepository.GetMyRates(rankingParams, userId);   
            
            Response.AddPagination(myRates.CurrentPage, myRates.PageSize, myRates.TotalCount, myRates.TotalPages);

            return Ok(myRates);
        }

        [AllowAnonymous]
        [HttpGet("myartistsrates")]
        public async Task<IActionResult> GetMyArtistsRates([FromQuery]RankingParams rankingParams)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var myRates = await appRepository.GetMyArtistsRates(rankingParams, userId);   
            
            Response.AddPagination(myRates.CurrentPage, myRates.PageSize, myRates.TotalCount, myRates.TotalPages);

            return Ok(myRates);
        }
    }
}