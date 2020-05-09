using System;

namespace SpotifyApp.API.Dtos
{
    public class CommentToReturnDto
    {
        public int Id { get; set; }
        public int CommenterId { get; set; }
        public string CommenterUsername { get; set; }
        public string CommenterPhotoUrl { get; set; }
        public string AlbumId { get; set; }
        public string CommentContent { get; set; }
        public DateTime CommentSent { get; set; }
    }
}