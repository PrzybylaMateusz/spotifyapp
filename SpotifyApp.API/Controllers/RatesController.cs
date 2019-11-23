using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpotifyApp.API.Data;
using SpotifyApp.API.Dtos;
using SpotifyApp.API.Models;

namespace SpotifyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAppRepository _appRepository;

        public RatesController(IMapper mapper, IAppRepository appRepository)
        {
            _mapper = mapper;
            _appRepository = appRepository;
        }

        [HttpPost("rate")]
        public async Task<IActionResult> RateAlbum(AlbumRateDto albumRateDto)
        {
            var albumRate = _mapper.Map<AlbumRate>(albumRateDto);
            await _appRepository.RateAlbum(albumRate);

            return StatusCode(201);
        }
    }
}