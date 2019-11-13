using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hhb.Common.Entities;
using Hhb.Common.Interfaces;
using Hhb.Repository.DTO;

namespace Hhb.Repository.MongoDB
{
    public static class Extentions
    {
        public static async Task<Fund> ToEntityAsync(
            this FundDTO item,
            IRepository<Transaction> transactionsRepository, 
            CancellationToken token) => 
            new Fund(
                new Identificator(item.Id),
                item.Name,
                item.Description,
                item.TransactionsIds != null ?
                await transactionsRepository.GetByIdsAsync(
                    item?.TransactionsIds?.Select(t => new Identificator(t)).ToArray() , token) : null);

        public static async Task<Transaction> ToEntityAsync(
            this TransactionDTO item,
            IRepository<TransactionType> transactionTypeRepository,
            CancellationToken token) =>
            new Transaction(
                new Identificator(item.Id),
                item.Name,
                item.PlannedSum,
                item.FactSum,
                item.DateTime,
                await transactionTypeRepository.GetByIdAsync(new Identificator(item.TransactionTypeId), token));

        public static async Task<TransactionType> ToEntityAsync(
            this TransactionTypeDTO item,
            IRepository<TransactionType> transactionTypeRepository,
            CancellationToken token) => 
            new TransactionType(
                new Identificator(item.Id),
                item.Name,
                await transactionTypeRepository.GetByIdAsync(new Identificator(item.ReverseTypeId), token),
                (TypeVariation)Enum.GetValues(typeof(TypeVariation)).GetValue(item.TypeVariationId));

        public static async Task<Route> ToEntityAsync(
            this RouteDTO item,
            IRepository<Transaction> transactionRepository,
            IRepository<Fund> fundRepository,
            CancellationToken token) => new Route(
                new Identificator(item.Id),
                await fundRepository.GetByIdAsync(new Identificator(item.SourceFundId), token),
                await fundRepository.GetByIdAsync(new Identificator(item.ReceiverFundId), token),
                await transactionRepository.GetByIdAsync(new Identificator(item.SourceTransactionId), token),
                await transactionRepository.GetByIdAsync(new Identificator(item.ReceiverTransactionId), token));
            
    }
}
