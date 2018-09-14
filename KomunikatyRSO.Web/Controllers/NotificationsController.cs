using System.Threading.Tasks;
using KomunikatyRSO.Shared.Commands;
using KomunikatyRSO.Shared.Commands.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace KomunikatyRSO.Web.Controllers
{
    public class NotificationsController : ApiControllerBase
    {

        public NotificationsController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdatePushChannel([FromBody] UpdatePushChannel command)
        {
            await DispatchAsync(command);

            return Ok();
        }

        [Authorize]
        [HttpPut("preferences")]
        public async Task<IActionResult> UpdatePreferences([FromBody] UpdatePreferences command)
        {
            await DispatchAsync(command);

            return Ok();
        }
    }
}
