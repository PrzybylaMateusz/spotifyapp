using System;

namespace SpotifyApp.API.Dtos
{
    public class CommentForCreationDto
    {
        public int CommenterId { get; set; }
        public string AlbumId { get; set; }
        public DateTime CommentSent { get; set; }
        public string CommentContent { get; set; }
        public CommentForCreationDto()
        {
            CommentSent = DateTime.Now;
        }
    }
}