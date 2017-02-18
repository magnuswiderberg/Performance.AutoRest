using Facade.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Facade.AutoRestClients.Api;
using System.Threading.Tasks;

namespace Facade.Business
{
    internal class PersonsHttpClientHandler : IPersonsHandler, IDisposable
    {

        public async Task<Person> GetByIdAsync(string id)
        {
            var result = await GetManyAsync();
            var person = result.FirstOrDefault(x => x.Id == id);
            return person;
        }

        public async Task<IEnumerable<Person>> GetManyAsync()
        {
            var result = await CreateApiClient().Persons.GetManyAsync();
            var converted = result.Select(Person.From).ToArray();
            return converted;
        }

        private static AutoRestClientsOverride.ApiClient CreateApiClient()
        {
            var client = new AutoRestClientsOverride.ApiClient(new Uri(ConfigurationManager.AppSettings["ApiBaseUrl"]), null);
            return client;
        }

        public void Dispose()
        {
            // Nothing to dispose, I suppose
        }

    }
}