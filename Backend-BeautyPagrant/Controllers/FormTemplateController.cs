using Backend_BeautyPagrant.Dto;
using Backend_BeautyPagrant.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend_BeautyPagrant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly BeautyPagrantContext _context;

        public FormController(BeautyPagrantContext context)
        {
            _context = context;
        }

        [HttpPost("create/{formTemplateId}")]
        public IActionResult CreateForm(int formTemplateId)
        {
            string userName = User.Identity?.Name ?? "System";

            Form form = Form.Create(_context, formTemplateId, userName);
            return Ok(new { form.Id, form.FormTemplateId });
        }

        [HttpPost("submit")]
        public IActionResult SubmitForm([FromBody] FormSubmitDto dto)
        {
            string userName = User.Identity?.Name ?? "User";

            Form? form = _context.Forms.FirstOrDefault(f => f.Id == dto.FormId);
            if (form == null)
            {
                return NotFound(new { message = "ไม่พบฟอร์ม" });
            }

            form.Submit(_context, dto, userName);
            return Ok(new { message = "บันทึกข้อมูลเรียบร้อยแล้ว", form.Id });
        }


        [HttpGet("{formId}")]
        public IActionResult GetForm(int formId)
        {
            object? result = Form.GetForm(_context, formId);
            if (result == null)
            {
                return NotFound(new { message = "ไม่พบฟอร์ม" });
            }

            return Ok(result);
        }

        [HttpGet("template/{formTemplateId}")]
        public IActionResult GetTemplate(int formTemplateId)
        {
            object? template = Form.GetTemplate(_context, formTemplateId);
            if (template == null)
            {
                return NotFound(new { message = "ไม่พบฟอร์ม Template นี้" });
            }

            return Ok(template);
        }
    }
}
