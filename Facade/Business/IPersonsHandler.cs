using Facade.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Facade.Business
{
    public interface IPersonsHandler
    {
        Task<Person> GetByIdAsync(string id);
        Task<IEnumerable<Person>> GetManyAsync();
    }
}
