using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Serilog.AWS.Logging.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> _logger;
        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
        }
        [HttpGet("{id:int}")]
        public IActionResult GetAsync(int id)
        {
            _logger.LogDebug("Received Request with id as {id}", id);
            _logger.LogInformation("Processing your request");
            _logger.LogError("Some Errors occcured.");
            return Ok("Logged");
        }
    }
}
