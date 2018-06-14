using System.Collections.Generic;

namespace Acerola.Domain
{
    public interface IAggregateRoot : IEntity
    {
        Dictionary<string, object> Export();
    }
}