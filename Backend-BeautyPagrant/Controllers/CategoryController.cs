using Backend_BeautyPagrant.Dto;
using Backend_BeautyPagrant.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_BeautyPagrant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly BeautyPagrantContext _context;

        public CategoryController(BeautyPagrantContext customContext)
        {
            _context = customContext;
        }

        [HttpGet("GetAll")]
        public ActionResult<List<Category>> GetAll()
        {
            List<Category> categories = Category.GetAll(_context);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetById(int id)
        {
            Category? category = Category.GetById(_context, id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost("Create")]
        public ActionResult<Category> Create([FromBody] CategoryCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string userName = User.Identity?.Name ?? "System";
            Category category = Category.Create(_context, dto, userName);
            _context.SaveChanges();
            return Ok(category);
        }

        [HttpPut("Update")]
        public ActionResult Update([FromBody] List<CategoryCreateDto> dtoList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string userName = User.Identity?.Name ?? "System";
            Category.Update(_context, dtoList,userName);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Category? category = Category.GetById(_context, id);
            if (category == null)
            {
                return NotFound();
            }
            string userName = User.Identity?.Name ?? "System";
            category.Delete(userName);
            _context.SaveChanges();
            return Ok();
        }

    }
}
