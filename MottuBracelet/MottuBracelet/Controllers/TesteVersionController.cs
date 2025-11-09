using Microsoft.AspNetCore.Mvc;

namespace MottuBracelet.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/teste")]
    public class TesteVersionController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetVersao()
        {
            return Ok(new
            {
                message = "Versão da API 1.0 funcionando ✅",
                timestamp = DateTime.UtcNow
            });
        }
    }
}
