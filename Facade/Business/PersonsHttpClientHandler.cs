using Facade.AutoRestClients.Api;
using Facade.Models;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Facade.Business
{
    internal class PersonsHttpClientHandler : IPersonsHandler, IDisposable
    {

        public Person GetById(string id)
        {
            var result = GetMany();
            var person = result.Where(x => x.Id == id).FirstOrDefault();
            return person;
        }

        public IEnumerable<Person> GetMany()
        {
            var result = CreateApiClient().Persons.GetMany();
            var converted = result.Select(x => Person.From(x)).ToArray();
            return converted;
        }

        private static ApiClient CreateApiClient()
        {
            var credentials = new BasicAuthenticationCredentials();
            var client = new ApiClient(new Uri(ConfigurationManager.AppSettings["ApiBaseUrl"]), credentials);
            return client;
        }

        public void Dispose()
        {
            // Nothing to dispose, I suppose
        }

    }
}