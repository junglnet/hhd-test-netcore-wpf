using MongoDB.Driver;

namespace Hhb.Repository.MongoDB
{
    public sealed class MongoRepository<T>
    {

        private readonly IMongoClient _mongoClient;

        private readonly IMongoDatabase _mongoDatabase;


        public MongoRepository(
            string collectionName,
            string connectionString,
            string databaseName)
        {
            ConnectionString = connectionString;

            DatabaseName = databaseName;
                       
            _mongoClient = new MongoClient(ConnectionString);

            _mongoDatabase = _mongoClient.GetDatabase(DatabaseName);

            Collection = _mongoDatabase.GetCollection<T>(collectionName);

        }

        public IMongoCollection<T> Collection { get; private set; }
        public string ConnectionString { get; private set; }
        public string DatabaseName { get; private set; }


    }
}
