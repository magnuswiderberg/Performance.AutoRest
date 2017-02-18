using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Services.AutoRestClients.Facade.Models;
using Services.Business;

namespace Services.Controllers
{
    [RoutePrefix("api/AutoRest/Persons")]
    public class PersonsAutoRestController : ApiController
    {
        [Route("")]
        [HttpGet]
        public async Task<IEnumerable<Person>> GetMany()
        {
            using (var handler = new PersonsAutoRestHandler())
            {
                var persons = await handler.GetManyAsync();
                return persons;
            }
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<Person> GetById(string id)
        {
            using (var handler = new PersonsAutoRestHandler())
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
