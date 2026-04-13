using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Serilog.AWS.Logging
{
    [Route("api/[controller]")]
    [ApiController]
    public class AWSSecretsController : ControllerBase
    {
    }
}
