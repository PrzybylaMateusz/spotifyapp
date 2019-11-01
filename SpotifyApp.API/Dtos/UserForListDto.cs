using System;

namespace SpotifyApp.API.Dtos
{
    public class UserForListDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime Created { get; set; }
        public string PhotoUrl { get; set; }
    }
}