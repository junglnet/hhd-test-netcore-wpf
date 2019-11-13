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
    public class MongoTransactionTypeRepository : IRepository<TransactionType>
    {

        private readonly MongoRepositoriesBundle _bundle;
       
        public MongoTransactionTypeRepository(
            MongoRepositoriesBundle bundle)
        {
            _bundle = bundle;
           
        }

        public async Task<Identificator> AddAsync(TransactionType item, CancellationToken token = default)
        {

            if (item.Id.ToString() == null)
                throw new Exception("Id is null");

            await _bundle.TransactionTypeRepository.Collection.InsertOneAsync(item.ToDTO(), null, token);

            return item.Id;
        }


        public async Task<bool> DeleteAsync(Identificator id, CancellationToken token = default)
        {

            var curItem = await GetByIdAsync(id, token);

            var deleteResult =
                await
                    _bundle.TransactionTypeRepository.Collection.DeleteOneAsync(
                        new ExpressionFilterDefinition<TransactionTypeDTO>(s => s.Id == id.ToString()), token);

            return deleteResult.IsAcknowledged && (deleteResult.DeletedCount == 1);

        }


        public async Task<IEnumerable<TransactionType>> GetAllAsync(CancellationToken token = default)
        {
            var asyncCursor = await (await _bundle.TransactionTypeRepository.Collection
                .FindAsync(FilterDefinition<TransactionTypeDTO>.Empty)).ToListAsync();

            var transactions = new List<TransactionType>();

            foreach (TransactionTypeDTO item in asyncCursor)
                transactions.Add(await item.ToEntityAsync(this, token));

            return transactions;
        }


        public async Task<TransactionType> GetByIdAsync(Identificator id, CancellationToken token = default) =>
            await (
                await (
                    await _bundle.TransactionTypeRepository.Collection
                    .FindAsync(s => s.Id == id.ToString()))
                .FirstOrDefaultAsync())
            .ToEntityAsync(this, token);


        public async Task<IEnumerable<TransactionType>> GetByIdsAsync(Identificator[] ids, CancellationToken token = default)
        {
            var funds = new List<TransactionType>();

            foreach (Identificator id in ids)
            {
                if (await IsExistById(id, token))

                    funds.Add(
                        await (
                            await (
                                await _bundle.TransactionTypeRepository.Collection
                        .FindAsync(s => s.Id == id.ToString()))
                            .FirstOrDefaultAsync())
                                .ToEntityAsync(this, token));
            }

            return funds.GroupBy(x => x.Id).Select(x => x.First()).ToList();
        }


        public async Task<bool> UpdateAsync(TransactionType item, CancellationToken token = default)
        {
            var replaceResult =
                await
                    _bundle.TransactionTypeRepository.Collection.ReplaceOneAsync(
                        new ExpressionFilterDefinition<TransactionTypeDTO>(s => s.Id == item.Id.ToString()), item.ToDTO());

            return replaceResult.IsAcknowledged && (replaceResult.MatchedCount == 1);
        }


        public async Task<bool> IsExistById(Identificator id, CancellationToken token = default) =>

            (await _bundle.TransactionTypeRepository.Collection.CountDocumentsAsync(s => s.Id == id.ToString())) != 0;

    }
}
