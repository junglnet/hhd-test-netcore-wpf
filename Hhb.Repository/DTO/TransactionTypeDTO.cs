namespace Hhb.Repository.DTO
{
    public class TransactionTypeDTO
    {

        public TransactionTypeDTO(
            string id, 
            string name, 
            string reverseTypeId, 
            int typeVariationId)
        {

            Id = id;

            Name = name;

            ReverseTypeId = reverseTypeId;

            TypeVariationId = typeVariationId;

        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string ReverseTypeId { get; private set; }
        public int TypeVariationId { get; private set; }

    }
}
