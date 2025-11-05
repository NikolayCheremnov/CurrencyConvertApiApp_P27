using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConvertApiApp.Api
{
    [Route("api")]
    [ApiController]
    public class ServerStatusController : ControllerBase
    {
        // GET /api
        [HttpGet]
        public StringMessage GetStatus()
        {
            return new StringMessage(
                Message: "server is running", 
                Time: DateTime.UtcNow
            );
        }

        // GET /api/ping
        [HttpGet("ping")]
        public StringMessage Ping()
        {
            return new StringMessage(
                Message: "pong",
                Time: DateTime.UtcNow
            );
        }
    }
}
