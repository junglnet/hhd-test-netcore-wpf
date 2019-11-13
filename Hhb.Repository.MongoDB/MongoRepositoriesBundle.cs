using MongoDB.Bson;
using MongoDB.Driver;
using Hhb.Common.Entities;
using Hhb.Repository.DTO;

namespace Hhb.Repository.MongoDB
{
    public class MongoRepositoriesBundle
    {

        public MongoRepositoriesBundle(
            string connectionString,
            string databaseName)
        {

            TransactionRepository = new MongoRepository<TransactionDTO>("Transactions", connectionString, databaseName);

            FundRepository = new MongoRepository<FundDTO>("Funds", connectionString, databaseName);

            TransactionTypeRepository = new MongoRepository<TransactionTypeDTO>("TransactionType", connectionString, databaseName);

            TransactionRouteRepository = new MongoRepository<RouteDTO>("Route", connectionString, databaseName);
        }

        public MongoRepository<TransactionDTO> TransactionRepository { get; }
        public MongoRepository<FundDTO> FundRepository { get; }
        public MongoRepository<RouteDTO> TransactionRouteRepository { get; set; }
        public MongoRepository<TransactionTypeDTO> TransactionTypeRepository { get; }


    }
}
