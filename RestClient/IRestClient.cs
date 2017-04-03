using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Rest;

namespace Client
{
    public interface IRestClient
    {
        Uri BaseUri { get; set; }
        ServiceClientCredentials Credentials { get; }

        Task<TResponse> Get<TResponse>(string relativeUrl);
        Task<HttpOperationResponse<TResponse>> SendRequest<TResponse>(HttpMethod method, string relativeUrl, Dictionary<string, List<string>> customHeaders, CancellationToken cancellationToken, object instance = null);

    }
}
