using Facade.Business;
using Facade.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace Facade.Controllers
{
    [RoutePrefix("api/Persons")]
    public class PersonsController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IEnumerable<Person> GetMany()
        {
            using (var handler = new PersonsHandler())
            {
                var persons = handler.GetMany();
                return persons;
            }
        }

        [Route("{id}")]
        [HttpGet]
        public Person GetById(string id)
        {
            using (var handler = new PersonsHandler())
            {
                var person = handler.GetById(id);
                if (person == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                return person;
            }
        }
    }
}
