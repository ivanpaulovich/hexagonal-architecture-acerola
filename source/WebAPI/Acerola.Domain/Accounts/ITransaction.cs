namespace Acerola.Domain.Accounts
{
    using Acerola.Domain.ValueObjects;

    public interface ITransaction
    {
        Amount GetAmount();
        string GetDescription();
    }
}
