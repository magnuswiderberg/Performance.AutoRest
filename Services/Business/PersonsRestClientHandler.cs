using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Client;
using Services.AutoRestClients.Facade.Models;
using Services.Business;

namespace Facade.Business
{
    internal class PersonsRestClientHandler : IPersonsHandler, IDisposable
    {
        private static readonly Uri BaseUri = new Uri(ConfigurationManager.AppSettings["FacadeBaseUrl"]);
        private static readonly IRestClient RestClient = new RestClient(BaseUri, null);

        public async Task<Person> GetByIdAsync(string id)
        {
            var result = await GetManyAsync();
            var person = result.FirstOrDefault(x => x.Id == id);
            return person;
        }

        public async Task<IEnumerable<Person>> GetManyAsync()
        {
            const string relativeUrl = "api/Persons";
            var result = await RestClient.Get<IEnumerable<Person>>(relativeUrl);
            return result;
        }

        public void Dispose()
        {
            // Nothing to dispose, I suppose
        }

    }
}