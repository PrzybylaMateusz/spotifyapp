using System;

namespace SpotifyApp.API.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int CommenterId { get; set; }
        public User Commenter { get; set; }
        public string AlbumId { get; set; }
        public Album Album { get; set; }
        public string CommentContent { get; set; }
        public DateTime CommentSent { get; set; }
    }
}