using System.Web.Http;

namespace Api.Controllers
{
    [RoutePrefix("")]
    public class DefaultController : ApiController
    {
        [Route("")]
        [HttpGet]
        public object Index()
        {
            return new { Information = "The API service is up and running" };
        }

        [Route("api")]
        [HttpGet]
        public object Api()
        {
            return new { Information = "This is the API end point. Please use /swagger to inspect." };
        }
    }
}
