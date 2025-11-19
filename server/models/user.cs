using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace server.models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; }

        public List<Novel> Novels { get; set; } = new List<Novel>();
        public List<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}