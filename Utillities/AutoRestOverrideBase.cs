using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Rest;
using Newtonsoft.Json;

namespace Performance.AutoRest.Utilities
{
    public class AutoRestOverrideBase
    {
        /// <summary>
        /// We by-pass AutoRest and have our own <see cref="HttpClient"/>
        /// because creating a new each request is bad for performance.
        /// </summary>
        private static readonly HttpClient HttpClient = new HttpClient();

        public AutoRestOverrideBase(Uri baseUri, ServiceClientCredentials credentials)
        {
            BaseUri = baseUri;
            Credentials = credentials;

            // These settings are the same as in AutoRest
            SerializationSettings = new JsonSerializerSettings
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
            DeserializationSettings = new JsonSerializerSettings
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
        }

        public Uri BaseUri { get; set; }
        public ServiceClientCredentials Credentials { get; }

        public JsonSerializerSettings SerializationSettings { get; }
        public JsonSerializerSettings DeserializationSettings { get; }


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

        public static async Task<Exception> CreateExceptionFromResponse(HttpResponseMessage response, HttpRequestMessage request, string requestContent)
        {
            var ex = new HttpOperationException($"Operation returned an invalid status code '{response.StatusCode}'");
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

        public void Dispose()
        {
            // Nothing to dispose, I suppose
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="method"></param>
        /// <param name="relativeUrl">Does NOT starts with /, does not include host.</param>
        /// <param name="customHeaders"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="expectedStatusCode"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public async Task<HttpOperationResponse<TResponse>> SendRequest<TResponse>(HttpMethod method, int expectedStatusCode, string relativeUrl, Dictionary<string, List<string>> customHeaders, CancellationToken cancellationToken, object instance = null)
        {
            var baseUrl = BaseUri.AbsoluteUri;
            if (!baseUrl.EndsWith("/")) baseUrl += "/";
            if (relativeUrl.StartsWith("/")) relativeUrl = relativeUrl.Substring(1);
            var url = new Uri(new Uri(baseUrl), relativeUrl).ToString();

            var request = CreateRequest(method, url, customHeaders);

            string requestContent = null;
            if (instance != null)
            {
                requestContent = Microsoft.Rest.Serialization.SafeJsonConvert.SerializeObject(instance, SerializationSettings);
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

            var statusCode = response.StatusCode;
            cancellationToken.ThrowIfCancellationRequested();
            if ((int)statusCode != expectedStatusCode)
            {
                var ex = await CreateExceptionFromResponse(response, request, requestContent);
                throw ex;
            }
            var result = new HttpOperationResponse<TResponse>
            {
                Request = request,
                Response = response
            };
            if ((method == HttpMethod.Get && expectedStatusCode != 204) || method == HttpMethod.Put || method == HttpMethod.Post)
            {
                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    result.Body = Microsoft.Rest.Serialization.SafeJsonConvert.DeserializeObject<TResponse>(responseContent, DeserializationSettings);
                }
                catch (JsonException ex)
                {
                    request.Dispose();
                    response.Dispose();
                    throw new SerializationException("Unable to deserialize the response.", responseContent, ex);
                }
            }
            return result;
        }

    }
}
