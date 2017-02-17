using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Business
{
    internal class PersonsHandler : IDisposable
    {

        internal Person GetById(string id)
        {
            var person = GetMany().Where(x => x.Id == id).FirstOrDefault();
            return person;
        }

        internal IEnumerable<Person> GetMany()
        {
            var persons = new List<Person> {
                new Person { Id = "1", GivenName = "Etta", SurName = "Karlsson", Email = "etta@karlsson.se" },
                new Person { Id = "2", GivenName = "Duo", SurName = "Lind", Email = "duo@lind.se" },
                new Person { Id = "3", GivenName = "Triss", SurName = "Ahl", Email = "triss@ahl.se" },
                new Person { Id = "4", GivenName = "Karl", SurName = "Kula", Email = "karl@kula.se" },
            };
            return persons;
        }

        public void Dispose()
        {
            // Nothing to dispose, I suppose
        }

    }
}