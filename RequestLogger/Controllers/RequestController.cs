using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RequestLogger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly ILogger<RequestController> _logger;
        public RequestController(ILogger<RequestController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public ActionResult Post(object body)
        {
            _logger.LogInformation($"Request posted.\nBody: {body}");

            return Ok(body);
        }

        [HttpPut]
        public ActionResult Put(object body)
        {
            _logger.LogInformation($"Request updated.\nBody: {body}");

            return Ok(body);
        }
    }
}
