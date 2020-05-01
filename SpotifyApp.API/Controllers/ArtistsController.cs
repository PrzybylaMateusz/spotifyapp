using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyApp.API.Data;

namespace SpotifyApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAppRepository _repo;
        private readonly ISpotifyData _spotifyData;

        public ArtistsController(IAppRepository repo, IMapper mapper, ISpotifyData spotifyData)
        {
            _repo = repo;
            _mapper = mapper;
            _spotifyData = spotifyData;
        }

         [HttpGet("{id}")]
        public async Task<IActionResult> GetArtist(string id)
        {
            var artist = await _spotifyData.GetSpotifyArtist(id);
            return Ok(artist);
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