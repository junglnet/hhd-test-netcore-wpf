using System.Collections.Generic;

namespace Hhb.Common.Entities
{
    public sealed class Fund : DictionaryBase
    {

        public Fund(
            Identificator id, 
            string name, 
            string description,
            IEnumerable<Transaction> transactions) : base(id, name)
        {

            Description = description;

            Transactions = transactions;

        }

        public string Description { get; private set; }

        public IEnumerable<Transaction> Transactions { get; private set; }
    }
}
