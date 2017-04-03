using Facade.Business;
using Facade.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace Facade.Controllers
{
    [RoutePrefix("api/RestClient/Persons")]
    public class PersonsRestClientController : ApiController
    {
        [Route("")]
        [HttpGet]
        public async Task<IEnumerable<Person>> GetMany()
        {
            using (var handler = new PersonsRestClientHandler())
            {
                var persons = await handler.GetManyAsync();
                return persons;
            }
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<Person> GetById(string id)
        {
            using (var handler = new PersonsRestClientHandler())
            {
                var person = await handler.GetByIdAsync(id);
                if (person == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                return person;
            }
        }
    }
}
