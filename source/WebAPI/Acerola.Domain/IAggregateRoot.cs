namespace Acerola.Domain
{
    public interface IAggregateRoot : IEntity
    {
        int Version { get; }
    }
}