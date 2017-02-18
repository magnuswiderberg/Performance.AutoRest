using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Facade.AutoRestClients.Api;
using Facade.AutoRestClients.Api.Models;
using Microsoft.Rest;

namespace Facade.AutoRestClientsOverride
{
    public class ApiClientPersons : IPersons
    {
        private readonly ApiClient _client;

        public ApiClientPersons(ApiClient client)
        {
            _client = client;
        }

        public async Task<HttpOperationResponse<IList<Person>>> GetManyWithHttpMessagesAsync(Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = new CancellationToken())
        {
            const string relativeUrl = "api/Persons";
            var result = await _client.SendRequest<IList<Person>>(HttpMethod.Get, 200, relativeUrl, customHeaders, cancellationToken);
            return result;
        }

        public async Task<HttpOperationResponse<Person>> GetByIdWithHttpMessagesAsync(string id, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = new CancellationToken())
        {
            var relativeUrl = $"api/Persons/{id}";
            var result = await _client.SendRequest<Person>(HttpMethod.Get, 200, relativeUrl, customHeaders, cancellationToken);
            return result;
        }
    }
}