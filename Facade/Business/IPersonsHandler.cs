using Facade.Models;
using System.Collections.Generic;

namespace Facade.Business
{
    public interface IPersonsHandler
    {
        Person GetById(string id);
        IEnumerable<Person> GetMany();
    }
}
