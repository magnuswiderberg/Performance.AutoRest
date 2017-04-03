using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Rest;

namespace Client.Helpers
{
    public class JwtCredentials : ServiceClientCredentials
    {
        // TODO: Just hard coded for SEF (InternalSystemUser)
        private static string _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Im1hdGNoLXhsZW50IiwiU2VmU3VwcGxpZXIiOiJtYXRjaC14bGVudCIsInJvbGUiOiJJbnRlcm5hbFN5c3RlbVVzZXIiLCJuYmYiOjE0ODQ4MTg0NzgsImV4cCI6MTgwMTA0MjQ3OCwiaWF0IjoxNDg0ODE4NDc4LCJpc3MiOiJzZWxmIiwiYXVkIjoiaHR0cDovL3d3dy5zZWYuc2UifQ.cCH-YlauCMXK0MVhBdulQ9mdiFEjhzJj1KyAJehWBRA";
        

        public override async Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var token =_token;
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            await base.ProcessHttpRequestAsync(request, cancellationToken);
        }
    }
}
