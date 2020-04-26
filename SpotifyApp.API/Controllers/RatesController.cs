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
        private readonly IMapper _mapper;
        private readonly IAppRepository _appRepository;
        private readonly ISpotifyData _spotifyData;

        public RatesController(IMapper mapper, IAppRepository appRepository, ISpotifyData spotifyData)
        {
            _mapper = mapper;
            _appRepository = appRepository;
            _spotifyData = spotifyData;
        }

        [HttpPost("album")]
        public async Task<IActionResult> RateAlbum(AlbumRateDto albumRateDto)
        {
            if (albumRateDto.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var rate = await _appRepository.GetRate(albumRateDto.UserId, albumRateDto.AlbumId);
            
            if(rate != null)
            {
                rate.Rate = albumRateDto.Rate;
                rate.RatedDate = albumRateDto.RatedDate;
                if(await _appRepository.SaveAll())
                {
                    return Ok();
                }
                return BadRequest("Failed to rate album");
            }

            if(await _appRepository.GetAlbum(albumRateDto.AlbumId) == null)
            {
                _appRepository.AddAlbum(new Album {Id = albumRateDto.AlbumId});
            }

            rate = _mapper.Map<AlbumRate>(albumRateDto);

            _appRepository.Add<AlbumRate>(rate);
             if(await _appRepository.SaveAll())
                {
                    return Ok();
                }
                return BadRequest("Failed to rate album");
        }
        
        [AllowAnonymous]
        [HttpGet("{albumId}/{userId}")]
        public async Task<IActionResult> GetAlbumRateForUser(string albumId, int userId)
        {
            var rate = await _appRepository.GetAlbumRateForUser(albumId, userId);
            return Ok(rate);
        }


        [AllowAnonymous]
        [HttpGet("albumranking")]
        public async Task<IActionResult> GetAlbumRanking([FromQuery]RankingParams rankingParams)
        {
            var allRates = await _appRepository.GetAllAlbumsRate(rankingParams);   
            
            Response.AddPagination(allRates.CurrentPage, allRates.PageSize, allRates.TotalCount, allRates.TotalPages);

            return Ok(allRates);
        }

        [AllowAnonymous]
        [HttpGet("myrates")]
        public async Task<IActionResult> GetMyRates([FromQuery]RankingParams rankingParams)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var myRates = await _appRepository.GetMyRates(rankingParams, userId);   
            
            Response.AddPagination(myRates.CurrentPage, myRates.PageSize, myRates.TotalCount, myRates.TotalPages);

            return Ok(myRates);
        }
    }
}