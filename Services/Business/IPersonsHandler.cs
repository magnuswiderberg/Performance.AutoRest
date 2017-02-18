using Services.AutoRestClients.Facade.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Business
{
    public interface IPersonsHandler
    {
        Task<Person> GetByIdAsync(string id);
        Task<IEnumerable<Person>> GetManyAsync();
    }
}
