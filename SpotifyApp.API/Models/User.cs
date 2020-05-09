using System;
using System.Collections.Generic;

namespace SpotifyApp.API.Models
{
    public class User
    {        
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; } 
        public byte[] PasswordSalt { get; set; } 
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; }
        public string About { get; set; }
        public ICollection<AlbumRate> AlbumRates { get; set; }
        public ICollection<ArtistRate> ArtistRates { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Photo Photo { get; set;}
    }
}