using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.models
{
    public class Novel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Chapter> Chapters { get; set; } = new List<Chapter>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}