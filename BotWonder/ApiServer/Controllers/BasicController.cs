using Microsoft.AspNetCore.Mvc;

namespace BotWonder.ApiServer.Controllers
{
    /// <summary>
    /// 基本的控制器
    /// </summary>
    [ApiController]
    [Route("api/bot")]
    public class BasicController : ControllerBase
    {
        /// <summary>
        /// ping
        /// </summary>
        /// <returns>pong</returns>
        [HttpGet("ping")]
        public string Ping()
        {
            return "pong";
        }
    }
}
