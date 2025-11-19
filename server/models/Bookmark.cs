using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.models
{
    public class Bookmark
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int NovelId { get; set; }
        public int LastChapterReadId { get; set; }
        public DateTime BookmarkedAt { get; set; }

        public User? User { get; set; }
        public Novel? Novel { get; set; }
        public Chapter? LastChapterRead { get; set; }
    }
}
