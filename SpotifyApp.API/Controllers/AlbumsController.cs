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
        public AlbumsController(IAppRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;

        }
        // GET api/values
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAlbums()
        {
            var albums = await _repo.GetAlbums();

            var albumsToReturn = _mapper.Map<IEnumerable<AlbumDto>>(albums);

            return Ok(albumsToReturn);
        }

        // GET api/values/5
        // Also possible to remove later
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbum(string id)
        {
            var album = await _repo.GetAlbum(Guid.Parse(id));

            var albumToReturn = _mapper.Map<AlbumDto>(album);

            return Ok(albumToReturn);
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
