using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Services.AutoRestClients.Facade;
using Services.AutoRestClients.Facade.Models;
using Services.Business;

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
            var result = await CreateApiClient().PersonsHttpClient.GetManyAsync();
            return result;
        }

        private static Services.AutoRestClientsOverride.FacadeClient CreateApiClient()
        {
            var client = new Services.AutoRestClientsOverride.FacadeClient(new Uri(ConfigurationManager.AppSettings["FacadeBaseUrl"]), null);
            return client;
        }

        public void Dispose()
        {
            // Nothing to dispose, I suppose
        }

    }
}