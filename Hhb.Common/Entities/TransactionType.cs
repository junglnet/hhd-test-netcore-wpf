
namespace Hhb.Common.Entities
{
    public sealed class TransactionType : DictionaryBase
    {
        
        public TransactionType(
            Identificator id, 
            string name, 
            TransactionType reverseType, 
            TypeVariation typeVariation) : base (id, name)
        {

            ReverseType = reverseType;

            TypeVariation = typeVariation;

        }

        public TransactionType ReverseType { get; private set; }

        public TypeVariation TypeVariation { get; private set; }

    }

    public enum TypeVariation
    {
        /// <summary>
        /// Сальдо
        /// </summary>
        Balance = 0,

        /// <summary>
        /// Расходная операция
        /// </summary>
        Expense = 1,

        /// <summary>
        /// Доходная операция
        /// </summary>
        Income = 2
    }

}
