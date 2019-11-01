using System;
using System.Collections.Generic;
using SpotifyApp.API.Models;

namespace SpotifyApp.API.Dtos
{
    public class UserForDetailedDto
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime Created { get; set; }
        public string About { get; set; }
        public ICollection<AlbumDto> Albums { get; set; }
        public string PhotoUrl { get; set; }
    }
}