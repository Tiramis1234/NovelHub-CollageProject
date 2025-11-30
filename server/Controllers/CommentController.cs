using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.data;
using server.Dtos;
using server.models;
using System.Linq;
using System.Threading.Tasks;

namespace server.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public CommentController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("{novelName}")]
        public async Task<IActionResult> GetByTitle([FromRoute] string novelName)
        {
            string decodedTitle = novelName.Replace("-", " ");

            var novel = await _context.Novels
                .FirstOrDefaultAsync(n => n.Title == decodedTitle);

            if (novel == null)
            {
                return NotFound(new { message = "Novel not found" });
            }

            var comments = await _context.Comments
                .Where(c => c.NovelId == novel.Id && !c.IsDeleted) 
                .Include(c => c.User) 
                .OrderByDescending(c => c.CreatedAt) 
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    Content = c.Content,
                    Username = c.User != null ? c.User.Username : "Deleted User", 
                    CreatedAt = c.CreatedAt,
                    ParentCommentId = c.ParentCommentId,
                    ImageUrl = c.ImageUrl
                })
                .ToListAsync();

            return Ok(comments);
        }
    }
}