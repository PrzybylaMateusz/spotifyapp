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
using SpotifyApp.API.Helpers;

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
    }
}