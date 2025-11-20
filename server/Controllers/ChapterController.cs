using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.data;
using server.Dtos;

namespace server.Controllers
{
    [Route("api/chapter")]
    [ApiController]
    public class ChapterController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ChapterController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("novel/{novelName}/chapter/{chapterNumber}")]
        public async Task<IActionResult> GetChapter([FromRoute] string novelName, [FromRoute] int chapterNumber)
        {
            string decodedTitle = novelName.Replace("-", " ");

        
            var novel = await _context.Novels.FirstOrDefaultAsync(n => n.Title == decodedTitle);
            if (novel == null) return NotFound("Nie znaleziono noweli");

       
            var currentChapter = await _context.Chapters
                .FirstOrDefaultAsync(c => c.NovelId == novel.Id && c.ChapterNumber == chapterNumber);

            if (currentChapter == null) return NotFound("Nie znaleziono rozdziału");

      
            bool hasNext = await _context.Chapters.AnyAsync(c => c.NovelId == novel.Id && c.ChapterNumber == chapterNumber + 1);
            bool hasPrev = await _context.Chapters.AnyAsync(c => c.NovelId == novel.Id && c.ChapterNumber == chapterNumber - 1);

     
            var dto = new ChapterReadDto
            {
                Id = currentChapter.Id,
                ChapterNumber = currentChapter.ChapterNumber,
                Title = currentChapter.Title, 
                Content = currentChapter.Content,
                NovelTitle = novel.Title, 
                NextChapterNumber = hasNext ? chapterNumber + 1 : null,
                PrevChapterNumber = hasPrev ? chapterNumber - 1 : null
            };

            return Ok(dto);
        }
    }
}