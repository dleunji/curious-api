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
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CodeActions;

namespace board.Controllers
{
    [EnableCors("LowCorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly BoardDbContext _context;

        public MembersController(BoardDbContext context)
        {
            _context = context;
        }

        // GET: api/Members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMember()
        {
            return await _context.Members.ToListAsync();
        }

        // GET: api/Members/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            var member = await _context.Members.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return member;
        }

        // PUT: api/Members/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember(int id, Member member)
        {
            if (id != member.MemberId)
            {
                return BadRequest();
            }

            _context.Entry(member).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
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

        // 회원가입
        [HttpPost("register")]
        public ActionResult<Member> RegisterMember(MemberRequest memberRequest)
        {

            var check = _context.Members
                .Any(m => m.MailAddress == memberRequest.MailAddress);
            if (check)
            {
                return BadRequest(new ErrorResponse(408, "Duplicated MailAddress"));
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(memberRequest.MemberPassword);
            var member = _context.Members
                .FromSqlInterpolated($"EXECUTE SP_RegisterMember {memberRequest.MemberName}, {hashedPassword}, {memberRequest.MailAddress}")
                .AsEnumerable()
                .FirstOrDefault();

            return Ok(member);
        }
        
        // 로그인
        [HttpPost("signin")]
        public ActionResult<Member> SignInMember(MemberRequest memberRequest)
        {
            var member = _context.Members
                .FromSqlInterpolated($"EXECUTE SP_SignInMember {memberRequest.MailAddress}")
                .AsEnumerable()
                .SingleOrDefault();
            
            // 가입되지 않은 정보
            if (member == null)
            {
                return NotFound();
            }

            var verified = BCrypt.Net.BCrypt.Verify(memberRequest.MemberPassword, member.MemberPassword);
            if (verified)
            {
                return Ok(member);
            }
            else
            {
                return BadRequest(new ErrorResponse(308, "Wrong Password"));
            }
            // TODO: 인증 성공 시 JWT 토큰 전달
        }

        // DELETE: api/Members/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.MemberId == id);
        }
    }
}
