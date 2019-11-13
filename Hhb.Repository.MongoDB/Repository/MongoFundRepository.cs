using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hhb.Common.Entities;
using Hhb.Common.Interfaces;
using Hhb.Repository.MongoDB.Exceptions;
using Hhb.Repository.DTO;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Hhb.Repository.MongoDB.Repository
{
    public class MongoFundRepository : IRepository<Fund>
    {
                
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IMongoCollection<FundDTO> _collection;

        public MongoFundRepository(
            MongoRepositoriesBundle bundle,
           IRepository<Transaction> transactionRepository)
        {
           
            _transactionRepository = transactionRepository;
            _collection = bundle.FundRepository.Collection;

        }
        
        public async Task<Identificator> AddAsync(Fund item, CancellationToken token = default)
        {

            if (item == null)
                throw new ArgumentNullException(nameof(item));
            
            await _collection.InsertOneAsync(item.ToDTO(), null ,token);

            return item.Id;

        }
        
        public async Task<bool> DeleteAsync(Identificator id, CancellationToken token = default)
        {
                        
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var deleteResult =
                await
                    _collection.DeleteOneAsync(
                        new ExpressionFilterDefinition<FundDTO>(s => s.Id == id.ToString()), token);

            return deleteResult.IsAcknowledged && (deleteResult.DeletedCount == 1);

        }
        
        public async Task<IEnumerable<Fund>> GetAllAsync(CancellationToken token = default)
        {
            
            var loadedList = await (await _collection
                .FindAsync(FilterDefinition<FundDTO>.Empty)).ToListAsync();

            return await Task.WhenAll(loadedList.Select(
                    async (item) => await item.ToEntityAsync(
                        _transactionRepository, token)));
        }
        
        public async Task<Fund> GetByIdAsync(Identificator id, CancellationToken token = default) {

            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var foundResult = 
                await (
                    await _collection
                    .FindAsync(s => s.Id == id.ToString()))
                    .FirstOrDefaultAsync();

            return foundResult == null ? 
                null : 
                await foundResult?.ToEntityAsync(_transactionRepository, token);

        }
        
        public async Task<IEnumerable<Fund>> GetByIdsAsync(Identificator[] ids, CancellationToken token = default)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

           
            var loadedList = await (await _collection.FindAsync(
                Builders<FundDTO>.Filter.AnyIn(
                    "_id", ids.Select(item => item.Id)))).ToListAsync();
                       
            return await Task.WhenAll(loadedList.Select(
                            async (item) => await item.ToEntityAsync(
                                _transactionRepository, token)).ToList());

            // return e.GroupBy(x => x.Id).Select(x => x.First()).ToList();
           
        }


        public async Task<bool> UpdateAsync(Fund item, CancellationToken token = default)
        {

            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var replaceResult =
                await
                    _collection.ReplaceOneAsync(s => s.Id == item.Id.ToString(), item.ToDTO());
            
            return replaceResult.IsAcknowledged && (replaceResult.MatchedCount == 1);
        }


        public async Task<bool> IsExistById(Identificator id, CancellationToken token = default) =>

            (await _collection.CountDocumentsAsync(s => s.Id == id.ToString())) != 0;

         

    }
}
