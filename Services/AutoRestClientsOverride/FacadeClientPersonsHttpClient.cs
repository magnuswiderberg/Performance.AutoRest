using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Rest;
using Services.AutoRestClients.Facade;
using Services.AutoRestClients.Facade.Models;

namespace Services.AutoRestClientsOverride
{
    internal class FacadeClientPersonsHttpClient : IPersonsHttpClient
    {
        private readonly FacadeClient _client;

        public FacadeClientPersonsHttpClient(FacadeClient client)
        {
            _client = client;
        }

        public async Task<HttpOperationResponse<IList<Person>>> GetManyWithHttpMessagesAsync(Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = new CancellationToken())
        {
            const string relativeUrl = "api/HttpClient/Persons";
            var result = await _client.SendRequest<IList<Person>>(HttpMethod.Get, 200, relativeUrl, customHeaders, cancellationToken);
            return result;
        }

        public async Task<HttpOperationResponse<Person>> GetByIdWithHttpMessagesAsync(string id, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = new CancellationToken())
        {
            var relativeUrl = $"api/HttpClient/Persons/{id}";
            var result = await _client.SendRequest<Person>(HttpMethod.Get, 200, relativeUrl, customHeaders, cancellationToken);
            return result;
        }
    }
}