using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotifyApp.API.Data;
using SpotifyApp.API.Dtos;

namespace SpotifyApp.API.Controllers
{
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
            ArtistWithAlbumsDto artist;
            try
            {
                artist = await spotifyData.GetSpotifyArtist(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
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