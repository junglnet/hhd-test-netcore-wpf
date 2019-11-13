using System;

namespace Hhb.Repository.DTO
{
    public class TransactionDTO 
    {


        public TransactionDTO(
            string id, 
            string name, 
            decimal plannedSum,            
            decimal factSum, 
            DateTime dateTime, 
            string transactionTypeId)
        {

            Id = id;

            Name = name;

            PlannedSum = plannedSum;

            FactSum = factSum;

            DateTime = dateTime;

            TransactionTypeId = transactionTypeId;

        }

        public string Id { get; private set; }

        public string Name { get; private set; }

        public decimal PlannedSum { get; private set; }

        public decimal FactSum { get; private set; }
                
        public DateTime DateTime { get; private set; }

        public string TransactionTypeId { get; private set; }

    }
}
