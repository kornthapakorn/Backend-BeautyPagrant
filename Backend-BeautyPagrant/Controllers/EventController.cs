using Backend_BeautyPagrant.Dto;
using Backend_BeautyPagrant.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Backend_BeautyPagrant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly BeautyPagrantContext _context;
        private readonly IConfiguration _config;

        public EventController(BeautyPagrantContext customContext, IConfiguration config)
        {
            _context = customContext;
            _config = config;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllEvents()
        {
            var events = Event.GetAll(_context);
            return Ok(events);
        }

        [HttpGet("{id}")]
        public IActionResult GetEventById(int id)
        {
            Event ev = Event.GetById(_context, id);
            if (ev == null)
                return NotFound(new { message = $"Event with id {id} not found" });

            return Ok(ev);
        }

        [HttpPost("create")]
        public ActionResult<EventDto> Create([FromBody] EventCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string userName = User.Identity?.Name ?? "System";
            string baseUrl = _config["FormSettings:BaseUrl"];

            Event ev = Event.Create(_context, dto, userName, baseUrl);
            _context.SaveChanges();

            return Ok(ev);
        }

        [HttpPost("Duplicate/{id}")]
        public ActionResult<EventDto> DuplicateEvent(int id)
        {
            string userName = User.Identity?.Name ?? "System";

            Event copy = Event.Duplicate(_context, id, userName);
            _context.SaveChanges();

            return Ok(copy);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] EventCreateDto dto)
        {
            string userName = User.Identity?.Name ?? "System";
            string baseUrl = _config["FormSettings:BaseUrl"];

            Event.Update(_context, id, dto, userName, baseUrl);
            _context.SaveChanges();

            return Ok(Event.GetById(_context, id));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            string userName = User.Identity?.Name ?? "System";

            Event.Delete(_context, id, userName);
            _context.SaveChanges();

            return Ok(new { message = "Event deleted successfully" });
        }

    }
}
