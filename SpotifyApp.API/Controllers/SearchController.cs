using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;

namespace SpotifyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        // private readonly string  _clientId = "69bbb47bc12a4a7cba51c70bc2ea6764";
        
        // [AllowAnonymous]
        // [HttpGet("{id}")]        
        // // public async Task<IActionResult> GetSearchResults()
        // public async Task<IActionResult> GetSearch(string id)
        // {

            

        //     return Ok("Jestem");
        // }

    }
}