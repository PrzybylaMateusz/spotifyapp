using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotifyApp.API.Data;
using SpotifyApp.API.Dtos;

namespace SpotifyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly ISpotifyData spotifyData;
        public AlbumsController(ISpotifyData spotifyData)
        {
            this.spotifyData = spotifyData;
        }      

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbum(string id)
        {
            AlbumDto album;
            try
            {
                album = await spotifyData.GetSpotifyAlbum(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

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
