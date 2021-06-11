using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TMF.Secure.API.SecretManagement;

namespace TMF.Secure.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("[controller]")]
    public class SecretsController : ControllerBase
    {
        private readonly ISecretManager _secretManager;

        public SecretsController(ISecretManager secretManager)
        {
            _secretManager = secretManager;
        }

        [HttpGet("", Name = "Get secret value by the secret name")]
        public async Task<IActionResult> GetAsync([FromQuery] string secretName)
        {
            if (string.IsNullOrEmpty(secretName))
            {
                return BadRequest();
            }

            string secretValue = await _secretManager.GetSecretAsync(secretName);

            if (!string.IsNullOrEmpty(secretValue))
            {
                return Ok(secretValue);
            }

            else
            {
                return Ok("There is no secret stored in the Key Vault");
            }
        }
    }
}
