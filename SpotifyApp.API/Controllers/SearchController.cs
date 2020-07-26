using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyApp.API.Data;
using SpotifyApp.API.Dtos;

namespace SpotifyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ISpotifyData spotifyData;
        public SearchController(IMapper mapper, ISpotifyData spotifyData)
        {
            this.mapper = mapper;
            this.spotifyData = spotifyData;
        }

        [AllowAnonymous]
        [HttpGet("artist/{id}")]
        public async Task<IActionResult> GetArtistSearch(string id)
        {
            var artists = await spotifyData.SearchSpotifyArtists(id);
            return Ok(artists);
        }

        [AllowAnonymous]
        [HttpGet("album/{id}")]
        public async Task<IActionResult> GetAlbumSearch(string id)
        {
            var albums = await spotifyData.SearchSpotifyAlbums(id);
            return Ok(albums);
        }
    }
}