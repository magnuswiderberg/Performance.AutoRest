using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Rest;
using Newtonsoft.Json;

namespace Client
{
    public class RestClient : IRestClient
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public RestClient(Uri baseUri, ServiceClientCredentials credentials)
        {
            BaseUri = baseUri;
            Credentials = credentials;

            #region Settings
            // These settings are the same as in AutoRest
            _serializationSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                ContractResolver = new Microsoft.Rest.Serialization.ReadOnlyJsonContractResolver(),
                Converters = new List<JsonConverter>
                    {
                        new Microsoft.Rest.Serialization.Iso8601TimeSpanConverter()
                    }
            };
            _deserializationSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                ContractResolver = new Microsoft.Rest.Serialization.ReadOnlyJsonContractResolver(),
                Converters = new List<JsonConverter>
                    {
                        new Microsoft.Rest.Serialization.Iso8601TimeSpanConverter()
                    }
            };
            #endregion
        }

        public Uri BaseUri { get; set; }
        public ServiceClientCredentials Credentials { get; }

        private readonly JsonSerializerSettings _serializationSettings;
        private readonly JsonSerializerSettings _deserializationSettings;

        #region Helpers

        private static HttpRequestMessage CreateRequest(HttpMethod method, string url, Dictionary<string, List<string>> customHeaders)
        {
            var request = new HttpRequestMessage(method, url);
            if (customHeaders != null)
            {
                foreach (var header in customHeaders)
                {
                    if (request.Headers.Contains(header.Key))
                    {
                        request.Headers.Remove(header.Key);
                    }
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }
            return request;
        }

        private static async Task<Exception> CreateExceptionFromResponse(HttpResponseMessage response, HttpRequestMessage request, string requestContent)
        {
            var ex = new HttpOperationException($"Operation returned an invalid status code '{response.StatusCode} for {response.RequestMessage.RequestUri}'");
            string responseContent;
            if (response.Content != null)
            {
                responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            else
            {
                responseContent = string.Empty;
            }
            ex.Request = new HttpRequestMessageWrapper(request, requestContent);
            ex.Response = new HttpResponseMessageWrapper(response, responseContent);
            request.Dispose();
            response.Dispose();
            throw ex;
        }

        #endregion

        public async Task<TResponse> Get<TResponse>(string relativeUrl)
        {
            var response = await SendRequest<TResponse>(HttpMethod.Get, relativeUrl, null, new CancellationToken());
            return response.Body;
        }

        public async Task<HttpOperationResponse<TResponse>> SendRequest<TResponse>(HttpMethod method, string relativeUrl,
            Dictionary<string, List<string>> customHeaders, CancellationToken cancellationToken,
            object instance = null)
        {
            var baseUrl = BaseUri.AbsoluteUri;
            if (!baseUrl.EndsWith("/")) baseUrl += "/";
            if (relativeUrl.StartsWith("/")) relativeUrl = relativeUrl.Substring(1);
            var url = new Uri(new Uri(baseUrl), relativeUrl).ToString();

            var request = CreateRequest(method, url, customHeaders);

            string requestContent = null;
            if (instance != null)
            {
                requestContent = Microsoft.Rest.Serialization.SafeJsonConvert.SerializeObject(instance, _serializationSettings);
                request.Content = new StringContent(requestContent, System.Text.Encoding.UTF8);
                request.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json; charset=utf-8");
            }

            if (Credentials != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Credentials.ProcessHttpRequestAsync(request, cancellationToken).ConfigureAwait(false);
            }
            cancellationToken.ThrowIfCancellationRequested();
            var response = await HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);

            cancellationToken.ThrowIfCancellationRequested();
            if (!response.IsSuccessStatusCode)
            {
                var ex = await CreateExceptionFromResponse(response, request, requestContent);
                throw ex;
            }
            var result = new HttpOperationResponse<TResponse>
            {
                Request = request,
                Response = response
            };
            if ((method == HttpMethod.Get && response.StatusCode != HttpStatusCode.NoContent) || method == HttpMethod.Put || method == HttpMethod.Post)
            {
                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    result.Body = Microsoft.Rest.Serialization.SafeJsonConvert.DeserializeObject<TResponse>(responseContent, _deserializationSettings);
                }
                catch (JsonException ex)
                {
                    request.Dispose();
                    response.Dispose();
                    throw new SerializationException("Unable to deserialize the response.", responseContent, ex);
                }
            }
            request.Dispose();
            response.Dispose();
            return result;
        }

    }
}
