using Backend_BeautyPagrant.Dto;
using Backend_BeautyPagrant.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Backend_BeautyPagrant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly BeautyPagrantContext _context;

        public ContactController(BeautyPagrantContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            List<Contact> contacts = Contact.GetAll(_context);

            if (contacts == null || contacts.Count == 0)
            {
                return Ok(new { title = string.Empty, links = new List<object>() });
            }

            Contact first = contacts.First();
            string baseUrl = $"{Request.Scheme}://{Request.Host}";

            var links = _context.ContactPictures
                .Where(p => p.ContactId == first.Id && !p.IsDelete)
                .OrderBy(p => p.Id)
                .Select(p => new
                {
                    image = (p.Image ?? string.Empty).StartsWith("http", StringComparison.OrdinalIgnoreCase)
                                ? p.Image
                                : $"{baseUrl}{p.Image}",   
                    url = p.Url
                })
                .ToList();

            return Ok(new { title = first.Title ?? string.Empty, links });
        }


        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            Contact? contact = Contact.GetById(_context, id);
            if (contact == null)
            {
                return NotFound(new { message = $"Contact with id {id} not found" });
            }

            List<ContactPictureDto> pics = _context.ContactPictures
                .Where(p => p.ContactId == contact.Id && !p.IsDelete)
                .OrderBy(p => p.Id)
                .Select(p => new ContactPictureDto
                {
                    Id = p.Id,
                    ImageId = p.Image ?? string.Empty,
                    Url = p.Url ?? string.Empty
                })
                .ToList();

            ContactDto dto = new ContactDto
            {
                Id = contact.Id,
                Title = contact.Title ?? string.Empty,
                Pictures = pics
            };

            return Ok(dto);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] ContactDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userName = User.Identity?.Name ?? "System";
            Contact contact = Contact.Create(_context, dto, userName);
            _context.SaveChanges();

            return Ok(new { id = contact.Id, message = "Created successfully" });
        }

        [HttpPut("Update/{id:int}")]
        public IActionResult Update(int id, [FromBody] ContactDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userName = User.Identity?.Name ?? "System";
            Contact.Update(_context, id, dto, userName);
            _context.SaveChanges();

            return Ok(new { message = "Updated successfully" });
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            string userName = User.Identity?.Name ?? "System";
            Contact.Delete(_context, id, userName);
            _context.SaveChanges();

            return Ok(new { message = "Deleted successfully" });
        }

        [HttpPost("UploadPicture/{contactId}")]
        public IActionResult UploadPicture(int contactId, IFormFile file, string? url)
        {
            Contact? contact = Contact.GetById(_context, contactId);
            if (contact == null)
            {
                return NotFound("Contact not found");
            }

            string userName = User.Identity?.Name ?? "System";

            ContactPicture picture = ContactPicture.Upload(
                _context,
                contact,
                file,
                url,
                userName,
                Directory.GetCurrentDirectory()
            );

            _context.SaveChanges();

            string baseUrl = Request.Scheme + "://" + Request.Host;
            return Ok(new
            {
                id = picture.Id,
                image = baseUrl + picture.Image,
                url = picture.Url,
                message = "Uploaded successfully"
            });
        }

    }
}
