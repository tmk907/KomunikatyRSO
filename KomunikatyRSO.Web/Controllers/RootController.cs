using Microsoft.AspNetCore.Mvc;

namespace KomunikatyRSO.Web.Controllers
{
    [Route("", Name = "default")]
    public class RootController : Controller
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
