using Hhb.Common.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hhb.Repository.MongoDB.Exceptions
{
    public class DataBaseException<T> : Exception
    {
                
        public DataBaseException(string ex, IMongoCollection<T> collection) : base (ex) =>
            Collection = collection;


        public IMongoCollection<T> Collection { get; }

    }
}
