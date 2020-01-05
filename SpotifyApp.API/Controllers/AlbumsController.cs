using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyApp.API.Data;
using SpotifyApp.API.Dtos;

namespace SpotifyApp.API.Controllers
{
    //Possible to remove later
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAppRepository _repo;
        private readonly ISpotifyData _spotifyData;
        public AlbumsController(IAppRepository repo, IMapper mapper, ISpotifyData spotifyData)
        {
            _repo = repo;
            _mapper = mapper;
            _spotifyData = spotifyData;
        }
        // GET api/values
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAlbums()
        {
            // var albums = await _repo.GetAlbums();
            
            var listOfAlbums = new List<string>();
            listOfAlbums.Add("1F8y2bg9V9nRoy8zuxo3Jt");
            listOfAlbums.Add("2Dnli6R27dyVX1GBLMudpN");
            listOfAlbums.Add("6b1HPtDuYioXwmw5xLLFQ9");
            listOfAlbums.Add("2T64N96AVfsrRFJCUXQEoZ");
            listOfAlbums.Add("79dL7FLiJFOO0EoehUHQBv");
            listOfAlbums.Add("1ZFjvEN3C2J1Q1xVhu2YaC");

            // var albumsToReturn = _mapper.Map<IEnumerable<AlbumDto>>(albums);
            var albums = await _spotifyData.GetSpotifyAlbums(listOfAlbums);
            var albumsToReturn = _mapper.Map<IEnumerable<AlbumDto>>(albums);
            return Ok(albumsToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbum(string id)
        {
            var album = await _spotifyData.GetSpotifyAlbum(id);
            return Ok(album);
        }
 

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
