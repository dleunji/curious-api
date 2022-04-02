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

namespace board.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly BoardDbContext _context;

        public CommentsController(BoardDbContext context)
        {
            _context = context;
        }
     

        // 댓글
        [HttpPost("comment")]
        public ActionResult PostCommentToA(CommentRequest comment)
        {
            var c = _context.Comments
                .FromSqlInterpolated($"EXECUTE SP_PostComment {comment.MemberId}, {comment.PostId}, {comment.Content}, {comment.PostType}")
                .AsEnumerable()
                .FirstOrDefault();

            return Ok(c);
        }


        // 댓글에 대한 답글
        [HttpPost("reply")]
        public ActionResult PostReplyToA(ReplyRequest reply)
        {
            var c = _context.Comments
                .FromSqlInterpolated($"EXECUTE SP_PostReply {reply.MemberId}, {reply.PostId}, {reply.Content}, {reply.ParentCommentId}")
                .AsEnumerable()
                .FirstOrDefault();

            return Ok(c);
        }

        // TODO: 페이지네이션, 포스트마다 가져오기

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            return await _context.Comments.ToListAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            if (id != comment.CommentId)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.CommentId == id);
        }
    }
}
