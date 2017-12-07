namespace Acerola.Domain
{
    public interface IAggregateRoot
    {
        int Version { get; }
    }
}