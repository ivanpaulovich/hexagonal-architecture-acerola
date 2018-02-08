using Acerola.Application.DTO;
using Acerola.Application.Mappers;
using Acerola.Domain.Accounts;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Acerola.Infrastructure.AutoMapper
{
    public class AccountsMapper : IAccountsMapper
    {
        public AccountData Map(Account account)
        {
            AccountData data = Mapper.Map<AccountData>(account);
            return data;
        }
    }
}
