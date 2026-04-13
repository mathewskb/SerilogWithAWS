using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.AspNetCore.Mvc;

namespace Serilog.AWS.Logging.Properties
{
    [Route("api/[controller]")]
    [ApiController]
    public class AWSSecretController(IAmazonSecretsManager _secretsManager,
         IConfiguration _config, ILogger<AWSSecretController> _logger) : ControllerBase
    {

        public async Task<IActionResult> GetSecretAsync()
        {

            _logger.LogDebug("Received Request");
            _logger.LogInformation("Processing request");

            var request = new GetSecretValueRequest
            {
                SecretId = _config["AWSSecretName"]
            };
            
            _logger.LogInformation("Processing request");
            _logger.LogDebug("Sending request to AWS Secrets Manager");

            var response = await _secretsManager.GetSecretValueAsync(request);
            _logger.LogDebug("Received response from AWS Secrets Manager");
            _logger.LogInformation("Processing response");

            if (!string.IsNullOrEmpty(response.SecretString))
            {
                _logger.LogInformation("Secret retrieved successfully");
                return Ok(response.SecretString);
            }

            _logger.LogError("Failed to retrieve secret");
            throw new Exception("Secret is empty");
        }
    }
}
