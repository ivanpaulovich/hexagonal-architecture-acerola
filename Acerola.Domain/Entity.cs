using System;

namespace Acerola.Domain
{
    public class Entity : IEntity
    {
        public Guid Id { get; protected set; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
