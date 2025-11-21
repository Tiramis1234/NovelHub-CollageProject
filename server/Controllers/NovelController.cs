using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.data;
using server.Dtos;

namespace server.Controllers
{
    [Route("api/novel")]
    [ApiController]
    public class NovelController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public NovelController(ApplicationDBContext context)
        {
            _context = context;
        }


        [HttpGet("{id:int}")] 
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
 
            var novel = await _context.Novels.FindAsync(id);
            if (novel == null) return NotFound();
            return Ok(novel);
        }

    
        [HttpGet("{novelName}")]
        public async Task<IActionResult> GetByTitle([FromRoute] string novelName)
        {
   
            string decodedTitle = novelName.Replace("-", " ");


            var novel = await _context.Novels
                .Include(n => n.Chapters)
                .FirstOrDefaultAsync(n => n.Title == decodedTitle);

            if (novel == null)
            {
                return NotFound(new { message = "Nie znaleziono noweli o takim tytule." });
            }

            var dto = new NovelDetailsDto
            {
                Id = novel.Id,
                Title = novel.Title,
                Description = novel.Description, 
                Chapters = novel.Chapters
                    .OrderBy(c => c.ChapterNumber)
                    .Select(c => new ChapterListItemDto
                    {
                        ChapterNumber = c.ChapterNumber,
                        Title = c.Title
                    }).ToList()
            };

            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
             var novels = await _context.Novels.ToListAsync();
             return Ok(novels);
        }
    }
}