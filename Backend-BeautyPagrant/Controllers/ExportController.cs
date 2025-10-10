using Backend_BeautyPagrant.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_BeautyPagrant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        private readonly ILogger<ExportController> _logger;
        private readonly BeautyPagrantContext context;

        public ExportController (ILogger<ExportController> logger, BeautyPagrantContext context)
        {
            _logger = logger;
            this.context = context;
        }
    }
}
