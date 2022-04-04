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
    public class CategoriesController : ControllerBase
    {
        private readonly BoardDbContext _context;

        public CategoriesController(BoardDbContext context)
        {
            _context = context;
        }
        
        // 카테고리 리스트 가져오기
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            return await _context.Categories
                .Where(c => c.ParentCategoryId == null)
                .Include(c => c.InverseParentCategory)
                .ThenInclude(c => c.InverseParentCategory)
                .ToListAsync();
        }
    }
}
