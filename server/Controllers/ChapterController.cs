using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.data;
using server.models;

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
        public IActionResult GetChapterByNovelAndNumber([FromRoute] string novelName, [FromRoute] int chapterNumber)
        {
            string searchTitle = novelName.Replace("-", " ");

            var chapter = _context.Chapters
                .Include(c => c.Novel)
                .AsEnumerable() 
                .FirstOrDefault(c => 
                   
                    string.Equals(c.Novel.Title.Replace("-", " "), searchTitle, StringComparison.OrdinalIgnoreCase) 
                    && c.ChapterNumber == chapterNumber);

            if (chapter == null)
            {
                return NotFound(new { message = $"Nie znaleziono noweli o nazwie: {novelName} lub rozdziału {chapterNumber}" });
            }

            return Ok(chapter);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var chapters = _context.Chapters.ToList();
            return Ok(chapters);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var chapter = _context.Chapters.Find(id);
            if (chapter == null)
            {
                return NotFound();
            }
            return Ok(chapter);
        }
    }
}