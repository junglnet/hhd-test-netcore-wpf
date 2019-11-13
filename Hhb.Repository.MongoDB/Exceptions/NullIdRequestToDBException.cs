using Hhb.Common.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hhb.Repository.MongoDB.Exceptions
{
    public class NullIdRequestToDBException<T> : DataBaseException<T>
    {
        public NullIdRequestToDBException(IMongoCollection<T> collection) : base("Идентификатор объекта пуст", collection) { }

    }
}
