using System.Web.Http;

namespace Facade.Controllers
{
    [RoutePrefix("")]
    public class DefaultController : ApiController
    {
        [Route("")]
        [HttpGet]
        public object Index()
        {
            return new { Information = "The Facade service is up and running" };
        }

        [Route("api")]
        [HttpGet]
        public object Api()
        {
            return new { Information = "This is the Facade end point. Please use /swagger to inspect." };
        }
    }
}
