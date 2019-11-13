
namespace Hhb.Common.Entities
{
    public class DictionaryBase : EntityBase
    {

        public DictionaryBase(Identificator id, string name) : base(id) =>
            Name = name;

        public string Name { get; protected set; }


    }
}
