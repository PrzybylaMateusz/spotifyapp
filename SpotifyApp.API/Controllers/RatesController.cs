using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyApp.API.Data;
using SpotifyApp.API.Dtos;
using SpotifyApp.API.Models;
using System.Linq;
using System.Collections.Generic;

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
            var albumRate = _mapper.Map<AlbumRate>(albumRateDto);
            await _appRepository.RateAlbum(albumRate);

            return StatusCode(201);
        }
        // [AllowAnonymous]
        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetSpecyficAlbumRate(string id)
        // {
        //     var albumRate = await _appRepository.GetSpecificAlbumAvaregeRate(Guid.Parse(id));
        //     return Ok(albumRate);
        // }

        [AllowAnonymous]
        [HttpGet("albumranking")]
        public async Task<IActionResult> GetAlbumRanking()
        {
            var allRates = await _appRepository.GetAllAlbumsRate();            
            var allGroupedRates = allRates.GroupBy(x => x.Album);
            var albumsInfoDto = await _spotifyData.GetSpotifyAlbums(allGroupedRates.Select(x => x.Key).ToList());
            var albumsInfo = _mapper.Map<IEnumerable<AlbumDto>>(albumsInfoDto);

            var albumRanking = new List<AlbumOverallRateDto>();

            foreach (var group in allGroupedRates)
            {
                var albumOverallRateDto = new AlbumOverallRateDto
                {
                    Album = albumsInfo.Where(x => x.Id == group.Key).FirstOrDefault(),
                    Rate = Math.Round(group.Average(x => x.Rate), 2)
                };
                albumRanking.Add(albumOverallRateDto);
            }

            var orderedAlbumRanking = albumRanking.OrderByDescending(x => x.Rate);
            return Ok(orderedAlbumRanking);
        }
    }
}