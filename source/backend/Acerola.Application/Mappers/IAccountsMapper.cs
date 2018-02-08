using Acerola.Application.DTO;
using Acerola.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Acerola.Application.Mappers
{
    public interface IAccountsMapper
    {
        AccountData Map(Account account);
    }
}
