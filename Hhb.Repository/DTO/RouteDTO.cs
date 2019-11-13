using Hhb.Common.Entities;

namespace Hhb.Repository.DTO
{
    public class RouteDTO
    {


        public RouteDTO(
            string id, 
            string sourceFundId, 
            string receiverFundId, 
            string sourceTransactionId, 
            string receiverTransactionId)
        {

            Id = id;

            SourceFundId = sourceFundId;

            ReceiverFundId = receiverFundId;

            SourceTransactionId = sourceTransactionId;

            ReceiverTransactionId = receiverTransactionId;

        }

        public string Id { get; private set; }        
        public string SourceFundId { get; private set; }

        public string ReceiverFundId { get; private set; }

        public string SourceTransactionId { get; private set; }

        public string ReceiverTransactionId { get; private set; }

    }
}
