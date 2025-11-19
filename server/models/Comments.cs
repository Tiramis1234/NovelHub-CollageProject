using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.models
{
    public class Comment
    {
        public int Id { get; set; }
        public int NovelId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public int? ParentCommentId { get; set; }
        public string? ImageUrl { get; set; }

        public Novel? Novel { get; set; }
        public User? User { get; set; }
        public Comment? ParentComment { get; set; }
    }
}
