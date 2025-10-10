using System;
using System.Collections.Generic;
using Backend_BeautyPagrant.Dto;
using Backend_BeautyPagrant.Models;
using Backend_BeautyPagrant.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Backend_BeautyPagrant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventpController : ControllerBase
    {
        private readonly BeautyPagrantContext _context;
        private readonly IConfiguration _config;
        private readonly IEventComponentImageBinder _imageBinder;

        public EventpController(BeautyPagrantContext customContext,IConfiguration config,IEventComponentImageBinder imageBinder)
        {
            _context = customContext;
            _config = config;
            _imageBinder = imageBinder;
        }
        [HttpPost("create-full")]
        [Consumes("multipart/form-data")]
        [DisableRequestSizeLimit]
        public IActionResult CreateFull([FromForm] string eventDto)
        {
            if (string.IsNullOrWhiteSpace(eventDto))
                return BadRequest("eventDto is required.");

            EventCreateDto dto;
            try
            {
                dto = JsonConvert.DeserializeObject<EventCreateDto>(eventDto);
            }
            catch (Exception)
            {
                return BadRequest("eventDto is invalid json.");
            }
            if (dto == null) return BadRequest("eventDto is invalid.");

            string userName = User?.Identity?.Name ?? "System";
            string baseUrl = _config["FormSettings:BaseUrl"];

            // 1) IFormFileCollection -> Dictionary<string, IFormFile>
            IDictionary<string, IFormFile> map = ToMap(Request.Form.Files);

            // (debug) ลองดูคีย์ที่ส่งมา
            // Console.WriteLine("UPLOAD KEYS: " + string.Join(", ", map.Keys));

            // 2) bind ไฟล์เข้า DTO
            _imageBinder.Bind(dto, map);

            // 3) สร้าง event + components
            Event ev = Event.Create(_context, dto, userName, baseUrl);
            _context.SaveChanges();

            return Ok(new { eventId = ev.Id });
        }

        private static IDictionary<string, IFormFile> ToMap(IFormFileCollection files)
        {
            Dictionary<string, IFormFile> dict = new Dictionary<string, IFormFile>(StringComparer.OrdinalIgnoreCase);
            if (files != null)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    IFormFile f = files[i];
                    if (f != null) dict[f.Name] = f; // เช่น "event.fileImage", "components[0].banner.image"
                }
            }
            return dict;
        }

    }
}
