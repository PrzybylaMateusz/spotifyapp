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
