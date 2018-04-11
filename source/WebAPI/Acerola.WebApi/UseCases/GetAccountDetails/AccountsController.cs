namespace Acerola.WebApi.UseCases.GetAccountDetails
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Acerola.Application.Queries;
    using Acerola.Application.Commands.Close;
    using Acerola.Application.Commands.Withdraw;
    using Acerola.Application.Commands.Deposit;
    using Acerola.WebApi.Model;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IAccountsQueries accountsQueries;

        public AccountsController(
            IAccountsQueries accountsQueries)
        {
            this.accountsQueries = accountsQueries;
        }

        /// <summary>
        /// Get an account balance
        /// </summary>
        [HttpGet("{accountId}", Name = "GetAccount")]
        public async Task<IActionResult> Get(Guid accountId)
        {
            var account = await accountsQueries.GetAccount(accountId);

            List<TransactionModel> transactions = new List<TransactionModel>();

            foreach (var item in account.Transactions)
            {
                var transaction = new TransactionModel(
                    item.Amount,
                    item.Description,
                    item.TransactionDate);

                transactions.Add(transaction);
            }

            return new ObjectResult(new AccountDetailsModel(
                account.AccountId,
                account.CurrentBalance,
                transactions));
        }
    }
}
