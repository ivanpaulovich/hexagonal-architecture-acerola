namespace Acerola.Application.Mappers
{
    using Acerola.Application.DTO;
    using Acerola.Domain.Accounts;

    public interface IAccountsMapper
    {
        AccountData Map(Account account);
    }
}
