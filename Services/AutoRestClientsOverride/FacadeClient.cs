using System;
using Microsoft.Rest;
using Performance.AutoRest.Utilities;
using Services.AutoRestClients.Facade;

namespace Services.AutoRestClientsOverride
{
    public class FacadeClient : AutoRestOverrideBase, IFacadeClient
    {
        public FacadeClient(Uri baseUri, ServiceClientCredentials credentials) : base(baseUri, credentials)
        {
            PersonsHttpClient = new FacadeClientPersonsHttpClient(this);
        }

        public IPersonsHttpClient PersonsHttpClient { get; }
        public IPersonsAutoRest PersonsAutoRest { get { throw new NotImplementedException(); } }
    }
}