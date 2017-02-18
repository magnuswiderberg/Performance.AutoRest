using Facade.Business;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Services.AutoRestClients.Facade.Models;

namespace Facade.Controllers
{
    [RoutePrefix("api/HttpClient/Persons")]
    public class PersonsHttpClientController : ApiController
    {
        [Route("")]
        [HttpGet]
        public async Task<IEnumerable<Person>> GetMany()
        {
            using (var handler = new PersonsHttpClientHandler())
            {
                var persons = await handler.GetManyAsync();
                return persons;
            }
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<Person> GetById(string id)
        {
            using (var handler = new PersonsHttpClientHandler())
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
