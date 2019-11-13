
namespace Hhb.Common.Entities
{
    public abstract class EntityBase
    {
        public Identificator Id { get; protected set; }

        protected EntityBase(Identificator id) =>
            Id = id;
    }
}
