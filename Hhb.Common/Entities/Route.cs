
namespace Hhb.Common.Entities
{
    public sealed class Route : EntityBase
    {

        public Route(
            Identificator id, 
            Fund sourceFund, 
            Fund receiverFund, 
            Transaction sourceTransaction, 
            Transaction receiverTransaction) : base(id)
        {

            SourceFund = sourceFund;

            ReceiverFund = receiverFund;

            SourceTransaction = sourceTransaction;

            ReceiverTransaction = receiverTransaction;

        }

        public Fund SourceFund { get; private set; }

        public Fund ReceiverFund { get; private set; }

        public Transaction SourceTransaction { get; private set; }

        public Transaction ReceiverTransaction { get; private set; }

    }
}
