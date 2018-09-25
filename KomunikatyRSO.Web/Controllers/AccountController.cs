using System.Threading.Tasks;
using KomunikatyRSO.Web.Infrastructure.Extensions;
using KomunikatyRSO.Shared.Commands;
using KomunikatyRSO.Shared.Commands.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using KomunikatyRSO.Web.Infrastructure.Handlers.Accounts;
using Microsoft.Extensions.Logging;

namespace KomunikatyRSO.Web.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly IMemoryCache cache;
        private readonly ILogger<AccountController> logger;
        private readonly ICommandHandler<ShowAccountList> al;

        public AccountController(ICommandDispatcher commandDispatcher, IMemoryCache cache, ILogger<AccountController> logger, ICommandHandler<ShowAccountList> al) : base(commandDispatcher)
        {
            this.cache = cache;
            this.logger = logger;
            this.al = al;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]Register command)
        {
            logger.LogInformation("Request recevied for account/register");
            await DispatchAsync(command);

            return Ok();
        }

        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody]CreateToken command)
        {
            logger.LogInformation("Request recevied for account/token");
            await DispatchAsync(command);
            var jwt = cache.GetJwt(command.UserId);
            return Json(jwt);
        }

        [HttpGet("all")]
        public async Task<IActionResult> List()
        {
            var list = await ((ShowAccountListHandler)al).HandleAsync2(new ShowAccountList());
            return new JsonResult(list);
        }
    }
}
