using Hhb.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hhb.Common.Interfaces
{
    public interface IFactory
    {        
        IRepository<Fund> FundRepository { get; }
        IRepository<Transaction> TransactionRepository { get; }
        IRepository<TransactionType> TransactionTypeRepository { get; }

    }
}
