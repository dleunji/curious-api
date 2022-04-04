using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using board;
using board.Models;
using board.Request;
using Microsoft.AspNetCore.Cors;

namespace board.Controllers
{
    [EnableCors("LowCorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly BoardDbContext _context;

        public QuestionsController(BoardDbContext context)
        {
            _context = context;
        }
        
        // ���� ����
        [HttpPost]
        public ActionResult PostQuestion([FromBody]QuestionRequest question)
        {
            var q = _context.Questions
                .FromSqlInterpolated($"EXECUTE SP_PostQuestion {question.MemberId}, {question.Title}, {question.Content}, {question.CategoryId}")
                .AsEnumerable()
                .FirstOrDefault();

            var member = _context.Members.SingleOrDefault(m => m.MemberId == question.MemberId);
            q.Member = member;

            return Ok(q);
        }

        // GET: api/Question
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        {
            return await _context.Questions
                .Include(q => q.Member)
                .ToListAsync();
        }

        [HttpGet("{questionId}")]
        public ActionResult<Question> GetQuestion(int questionId)
        {
            var question = _context.Questions
                .Where(q => q.QuestionId == questionId)
                .Include(q => q.Member)
                .FirstOrDefault();
            
                // .Include(q => q.Comments.Where(c => c.Depth == 0))
                // .ThenInclude(c => c.Member)
                // .Include(q => q.Comments)
                // .ThenInclude(c => c.InverseParentComment)
                // .ThenInclude(c => c.Member)
                // .FirstOrDefault();


                //    .ThenInclude(c => c.Member)
                // .Include(q => q.Member)
                // .FirstOrDefault();


            if (question == null)
            {
                return NotFound();
            }

            // var member = _context.Members.SingleOrDefault(m => m.MemberId == question.MemberId);
            // question.Member = member;

            return Ok(question);
        }

        // PUT: api/Question/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, Question question)
        {
            if (id != question.QuestionId)
            {
                return BadRequest();
            }

            _context.Entry(question).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
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

        // DELETE: api/Question/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.QuestionId == id);
        }
    }
}
