using System.Threading.Tasks;
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
        private readonly ISpotifyData spotifyData;

        public ArtistsController(ISpotifyData spotifyData)
        {
            this.spotifyData = spotifyData;
        }

         [HttpGet("{id}")]
        public async Task<IActionResult> GetArtist(string id)
        {
            var artist = await spotifyData.GetSpotifyArtist(id);
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