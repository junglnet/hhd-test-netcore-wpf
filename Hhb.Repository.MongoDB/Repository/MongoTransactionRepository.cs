using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hhb.Common.Entities;
using Hhb.Common.Interfaces;
using Hhb.Repository.DTO;
using MongoDB.Driver;


namespace Hhb.Repository.MongoDB.Repository
{
    public class MongoTransactionRepository : IRepository<Transaction>
    {

        private readonly MongoRepositoriesBundle _bundle;
        private IRepository<TransactionType> _transactionTypeRepository;

        public MongoTransactionRepository(
            MongoRepositoriesBundle bundle,
           IRepository<TransactionType> transactionTypeRepository)
        {
            _bundle = bundle;
            _transactionTypeRepository = transactionTypeRepository;

        }

        public async Task<Identificator> AddAsync(Transaction item, CancellationToken token = default)
        {

            if (item.Id.ToString() == null)
                throw new Exception("Id is null");

            await _bundle.TransactionRepository.Collection.InsertOneAsync(item.ToDTO(), null, token);

            return item.Id;
        }


        public async Task<bool> DeleteAsync(Identificator id, CancellationToken token = default)
        {

            var curItem = await GetByIdAsync(id, token);

            var deleteResult =
                await
                    _bundle.TransactionRepository.Collection.DeleteOneAsync(
                        new ExpressionFilterDefinition<TransactionDTO>(s => s.Id == id.ToString()), token);

            return deleteResult.IsAcknowledged && (deleteResult.DeletedCount == 1);

        }


        public async Task<IEnumerable<Transaction>> GetAllAsync(CancellationToken token = default)
        {
            var asyncCursor = await (await _bundle.TransactionRepository.Collection
                .FindAsync(FilterDefinition<TransactionDTO>.Empty)).ToListAsync();

            var transactions = new List<Transaction>();

            foreach (TransactionDTO item in asyncCursor)
                transactions.Add(await item.ToEntityAsync(_transactionTypeRepository, token));

            return transactions;
        }


        public async Task<Transaction> GetByIdAsync(Identificator id, CancellationToken token = default) =>
            await (
                await (
                    await _bundle.TransactionRepository.Collection
                    .FindAsync(s => s.Id == id.ToString()))
                .FirstOrDefaultAsync())
            .ToEntityAsync(_transactionTypeRepository, token);


        public async Task<IEnumerable<Transaction>> GetByIdsAsync(Identificator[] ids, CancellationToken token = default)
        {
            var funds = new List<Transaction>();

            foreach (Identificator id in ids)
            {
                if (await IsExistById(id, token))

                    funds.Add(
                        await (
                            await (
                                await _bundle.TransactionRepository.Collection
                        .FindAsync(s => s.Id == id.ToString()))
                            .FirstOrDefaultAsync())
                                .ToEntityAsync(_transactionTypeRepository, token));
            }

            return funds.GroupBy(x => x.Id).Select(x => x.First()).ToList();
        }


        public async Task<bool> UpdateAsync(Transaction item, CancellationToken token = default)
        {
            var replaceResult =
                await
                    _bundle.TransactionRepository.Collection.ReplaceOneAsync(
                        new ExpressionFilterDefinition<TransactionDTO>(s => s.Id == item.Id.ToString()), item.ToDTO());

            return replaceResult.IsAcknowledged && (replaceResult.MatchedCount == 1);
        }


        public async Task<bool> IsExistById(Identificator id, CancellationToken token = default) =>

            (await _bundle.TransactionRepository.Collection.CountDocumentsAsync(s => s.Id == id.ToString())) != 0;

    }
}
