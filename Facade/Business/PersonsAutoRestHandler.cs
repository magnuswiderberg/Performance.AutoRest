using Facade.AutoRestClients.Api;
using Facade.Models;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Facade.Business
{
    internal class PersonsAutoRestHandler : IPersonsHandler, IDisposable
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

        private static ApiClient CreateApiClient()
        {
            var client = new ApiClient(new Uri(ConfigurationManager.AppSettings["ApiBaseUrl"]), null);
            return client;
        }

        public void Dispose()
        {
            // Nothing to dispose, I suppose
        }

    }
}