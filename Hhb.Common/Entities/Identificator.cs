
using System;

namespace Hhb.Common.Entities
{
    public sealed class Identificator 
    {

        public Identificator(string id) =>
            Id = id;
        public string Id { get; }

        public override string ToString() => Id;

        public static Identificator GenerateNewId() =>
            new Identificator(Guid.NewGuid().ToString("N"));

    }
}
