using System.Threading.Tasks;
using KomunikatyRSO.Web.Infrastructure.Extensions;
using KomunikatyRSO.Shared.Commands;
using KomunikatyRSO.Shared.Commands.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace KomunikatyRSO.Web.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly IMemoryCache cache;

        public AccountController(ICommandDispatcher commandDispatcher, IMemoryCache cache) : base(commandDispatcher)
        {
            this.cache = cache;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]Register command)
        {
            await DispatchAsync(command);

            return Ok();
        }

        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody]CreateToken command)
        {
            await DispatchAsync(command);
            var jwt = cache.GetJwt(command.UserId);
            return Json(jwt);
        }

        [HttpPost("testjson")]
        public async Task<IActionResult> Test([FromBody]Register command)
        {
            return Ok();
        }

        [HttpPost("testjson2")]
        public async Task<IActionResult> Test2([FromBody]Register command)
        {
            return Ok(command.UserId);
        }
    }
}
