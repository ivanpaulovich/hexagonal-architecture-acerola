namespace Acerola.Infrastructure.Mappings
{
    using Acerola.Application.DTO;
    using Acerola.Application.Mappers;
    using Acerola.Domain.Accounts;
    using AutoMapper;

    public class AccountsMapper : IAccountsMapper
    {
        public AccountData Map(Account account)
        {
            AccountData data = Mapper.Map<AccountData>(account);
            return data;
        }
    }
}
