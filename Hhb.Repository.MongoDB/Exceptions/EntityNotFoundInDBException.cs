using Hhb.Common.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hhb.Repository.MongoDB.Exceptions
{
    public class EntityNotFoundInDBException<T> : DataBaseException<T>
    {
        public EntityNotFoundInDBException(IMongoCollection<T> collection) : base("Объект не найден в БД " + collection.ToString(), collection) { }

    }
}
