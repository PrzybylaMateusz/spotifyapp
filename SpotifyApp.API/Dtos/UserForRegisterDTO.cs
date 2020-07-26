using System.ComponentModel.DataAnnotations;

namespace SpotifyApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username {get; set;}

        [Required]
        [MinLength(8, ErrorMessage = "Password must has at least 8 characters")]
        public string Password {get; set;}
    }
}