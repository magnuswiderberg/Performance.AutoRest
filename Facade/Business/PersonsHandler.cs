using Facade.Models;
using System;
using System.Collections.Generic;

namespace Facade.Business
{
    internal class PersonsHandler : IDisposable
    {

        internal Person GetById(string id)
        {
            // TODO
            throw new NotImplementedException();
        }

        internal IEnumerable<Person> GetMany()
        {
            // TODO
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            // Nothing to dispose, I suppose
        }

    }
}