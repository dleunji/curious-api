using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using board;
using board.Models;

namespace board.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SympathyController : ControllerBase
    {
        private readonly BoardDbContext _context;

        public SympathyController(BoardDbContext context)
        {
            _context = context;
        }
        
        // 질문에 대한 공감하기
        [HttpPost]
        public async Task<ActionResult<Sympathy>> PostSympathy(Sympathy sympathy)
        {
            _context.Sympathies.Add(sympathy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSympathy", new { id = sympathy.SympathyId }, sympathy);
        }

        // GET: api/Sympathy
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sympathy>>> GetSympathies()
        {
            return await _context.Sympathies.ToListAsync();
        }

        // GET: api/Sympathy/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sympathy>> GetSympathy(int id)
        {
            var sympathy = await _context.Sympathies.FindAsync(id);

            if (sympathy == null)
            {
                return NotFound();
            }

            return sympathy;
        }

        // PUT: api/Sympathy/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSympathy(int id, Sympathy sympathy)
        {
            if (id != sympathy.SympathyId)
            {
                return BadRequest();
            }

            _context.Entry(sympathy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SympathyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Sympathy/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSympathy(int id)
        {
            var sympathy = await _context.Sympathies.FindAsync(id);
            if (sympathy == null)
            {
                return NotFound();
            }

            _context.Sympathies.Remove(sympathy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SympathyExists(int id)
        {
            return _context.Sympathies.Any(e => e.SympathyId == id);
        }
    }
}
