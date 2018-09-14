using Microsoft.AspNetCore.Mvc;

namespace KomunikatyRSO.Web.Controllers
{
    [Route("")]
    public abstract class RootController : Controller
    {
        public RootController()
        {
        }

        [HttpGet]
        public OkResult Get()
        {
            return Ok();
        }
    }
}
