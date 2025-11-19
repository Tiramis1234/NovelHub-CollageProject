using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.data;
using server.models;


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

        [HttpGet]
        public IActionResult GetAll()
        {
            var novels = _context.Novels.ToList();
            return Ok(novels);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var novel = _context.Novels.Find(id);
            if (novel == null)
            {
                return NotFound();
            }
            return Ok(novel);
        }
    }
}