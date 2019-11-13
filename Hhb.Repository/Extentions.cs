using System.Linq;
using Hhb.Common.Entities;
using Hhb.Repository.DTO;

namespace Hhb.Repository
{
    public static class Extentions
    {
        public static FundDTO ToDTO(this Fund item) => 
            new FundDTO(
                item.Id.ToString(), 
                item.Name, 
                item.Description, 
                item.Transactions?.Select(p => p.Id.ToString()).ToArray());

        public static TransactionDTO ToDTO(this Transaction item) =>
            new TransactionDTO(
                item.Id.ToString(),
                item.Name,
                item.PlannedSum,
                item.FactSum,
                item.DateTime,
                item.TransactionType.Id.ToString());

        public static TransactionTypeDTO ToDTO(this TransactionType item) =>
            new TransactionTypeDTO(
                item.Id.ToString(),
                item.Name,
                item.ReverseType.Id.ToString(),
                (int)item.TypeVariation);

        public static RouteDTO ToDTO(this Route item) =>
            new RouteDTO(
                item.Id.ToString(),
                item.SourceFund.Id.ToString(),
                item.ReceiverFund.Id.ToString(),
                item.SourceTransaction.Id.ToString(),
                item.ReceiverTransaction.Id.ToString());
    }
}
