using Hhb.Common.Entities;
using Hhb.Common.Interfaces;
using Hhb.Repository.MongoDB.Repository;
using System;

namespace Hhb.Repository.MongoDB.Test
{
    public sealed class TestFactoty : IFactory
    {

        private static readonly Lazy<TestFactoty> _current = new Lazy<TestFactoty>(() => new TestFactoty());
             
        private TestFactoty()
        {

            MongoRepositoriesBundle bundle = new MongoRepositoriesBundle("mongodb://localhost:27017", "HhbTest");

            var fundRepository = new MongoFundRepository(bundle, TransactionRepository);

            FundRepository = fundRepository;

            var transactionTypeRepository = new MongoTransactionTypeRepository(bundle);

            TransactionTypeRepository = transactionTypeRepository;

            var transactionRepository = new MongoTransactionRepository(bundle, TransactionTypeRepository);

            TransactionRepository = transactionRepository;

        }
    
       public static TestFactoty Current
       {
           get => _current.Value;           
       }

        public IRepository<Fund> FundRepository { get; }
        public IRepository<Transaction> TransactionRepository { get; }
        public IRepository<TransactionType> TransactionTypeRepository { get; }
    }
}
