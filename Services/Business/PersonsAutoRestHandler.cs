using Services.AutoRestClients.Facade;
using Services.AutoRestClients.Facade.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Rest;

namespace Services.Business
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
            var result = await CreateApiClient().PersonsAutoRest.GetManyAsync();
            return result;
        }

        private static FacadeClient CreateApiClient()
        {
            var credentials = new BasicAuthenticationCredentials();
            var client = new FacadeClient(new Uri(ConfigurationManager.AppSettings["FacadeBaseUrl"]), credentials);
            return client;
        }

        public void Dispose()
        {
            // Nothing to dispose, I suppose
        }

    }
}