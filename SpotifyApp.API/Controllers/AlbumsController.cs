using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpotifyApp.API.Data;

namespace SpotifyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly DataContext _context;
        public AlbumsController(DataContext context)
        {
            _context = context;

        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetAlbums()
        {
            var albums = await _context.Albums.ToListAsync();
            return Ok(albums);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbum(string id)
        {
            var album = await _context.Albums.FirstOrDefaultAsync(x => x.Id.ToString() == id);
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
