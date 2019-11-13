using System;

namespace Hhb.Common.Entities
{
    public sealed class Transaction : DictionaryBase
    {

        public Transaction(
            Identificator id, 
            string name, 
            decimal plannedSum, 
            decimal factSum, 
            DateTime dateTime,
            TransactionType transactionType
            ) : base(id, name)
        {

            PlannedSum = plannedSum;

            FactSum = factSum;

            DateTime = dateTime;

            TransactionType = transactionType;

        }
        /// <summary>
        /// Плановая сумма транзации
        /// </summary>
        public decimal PlannedSum { get; private set; }

        /// <summary>
        /// Фактическая сумма транзакции
        /// </summary>
        public decimal FactSum { get; private set; }

        /// <summary>
        /// Дата транзации
        /// </summary>
        public DateTime DateTime { get; private set; }

        public TransactionType TransactionType { get; private set; }
        
    }

    
}
