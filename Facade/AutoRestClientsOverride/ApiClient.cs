using System;
using Facade.AutoRestClients.Api;
using Microsoft.Rest;
using Performance.AutoRest.Utilities;

namespace Facade.AutoRestClientsOverride
{
    public class ApiClient : AutoRestOverrideBase, IApiClient
    {
        public ApiClient(Uri baseUri, ServiceClientCredentials credentials) : base(baseUri, credentials)
        {
            Persons = new ApiClientPersons(this);
        }

        public IPersons Persons { get; }

        public IDefaultModel DefaultModel { get { throw new NotImplementedException(); } }
    }
}