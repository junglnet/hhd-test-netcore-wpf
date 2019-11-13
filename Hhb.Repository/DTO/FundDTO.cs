using Hhb.Common.Entities;

namespace Hhb.Repository.DTO
{
    public class FundDTO 
    {


        public FundDTO(string id, string name, string description, string[] transactionIds) 
        {

            Id = id;

            Name = name;

            Description = description;

            TransactionsIds = transactionIds;

        }

        public string Id { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; }

        public string[] TransactionsIds { get; private set; }

    }
}
