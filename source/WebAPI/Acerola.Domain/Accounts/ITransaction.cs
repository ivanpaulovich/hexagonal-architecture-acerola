namespace Acerola.Domain.Accounts
{
    using Acerola.Domain.ValueObjects;

    public interface ITransaction
    {
        Amount Amount { get; }
        string Description { get; }
    }
}
