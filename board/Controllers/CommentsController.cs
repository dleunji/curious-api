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
        
        
        [HttpPost("comment")]
        public ActionResult PostCommentToA(CommentRequest comment)
        {
            var c = _context.Comments
                .FromSqlInterpolated($"EXECUTE SP_PostComment {comment.MemberId}, {comment.QuestionId}, {comment.Content}")
                .AsEnumerable()
                .FirstOrDefault();

            return Ok(c);
        }
        
        [HttpPost("reply")]
        public ActionResult PostReplyToA(ReplyRequest reply)
        {
            var c = _context.Comments
                .FromSqlInterpolated($"EXECUTE SP_PostReply {reply.MemberId}, {reply.QuestionId}, {reply.Content}, {reply.ParentCommentId}")
                .AsEnumerable()
                .FirstOrDefault();

            return Ok(c);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            return await _context.Comments.ToListAsync();
        }
        
        // 특정 질문의 댓글 리스트
        [HttpGet("questions/{questionId}/comments")]
        public ActionResult GetCommentsByQuestionId(int questionId)
        {
            var comments = _context.Comments
                .Where(c => c.QuestionId == questionId && c.Depth == 0)
                .Include(c => c.InverseParentComment)
                .ThenInclude(c => c.InverseParentComment)
                .ThenInclude(c => c.InverseParentComment)
                .ToList();

            return Ok(comments);
        }
        
        // 특정 질문에 댓글을 단 유저 리스트
        [HttpGet("questions/{questionId}/members")]
        public ActionResult GetMembersByQuestionId(int questionId)
        {
            var members = _context.Comments
                .Where(c => c.QuestionId == questionId)
                .Select(c => c.Member).Distinct();

            return Ok(members);
        }
        
    }
}
