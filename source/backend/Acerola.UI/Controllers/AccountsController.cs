namespace Acerola.UI.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Acerola.Application.Commands.Accounts;
    using Acerola.Application.Queries;
    using Acerola.Domain.Accounts;
    using System.Collections.Generic;
    using Acerola.Application.DTO;

    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IMediator mediator;
        private readonly IAccountsQueries accountsQueries;

        public int AccoutVM { get; private set; }

        public AccountsController(IMediator mediator, IAccountsQueries accountsQueries)
        {
            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator));

            if (accountsQueries == null)
                throw new ArgumentNullException(nameof(accountsQueries));

            this.mediator = mediator;
            this.accountsQueries = accountsQueries;
        }

        /// <summary>
        /// Deposit from an account
        /// </summary>
        [HttpPatch("Deposit")]
        public async Task<IActionResult> Deposit([FromBody]DepositCommand command)
        {
            Transaction transaction = await mediator.Send(command);
            return (IActionResult)Ok();
        }

        /// <summary>
        /// Withdraw from an account
        /// </summary>
        [HttpPatch("Withdraw")]
        public async Task<IActionResult> Withdraw([FromBody]WithdrawCommand command)
        {
            Transaction transaction = await mediator.Send(command);
            return (IActionResult)Ok();
        }

        /// <summary>
        /// Close an account
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Close([FromBody]CloseCommand command)
        {
            await mediator.Send(command);
            return (IActionResult)Ok();
        }

        /// <summary>
        /// Get an account balance
        /// </summary>
        [HttpGet("{id}", Name = "GetAccount")]
        public async Task<IActionResult> Get(Guid id)
        {
            var account = await accountsQueries.GetAccount(id);

            return Ok(account);
        }

        /// <summary>
        /// List all accounts
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> List([FromQuery]Guid? customerId)
        {
            IEnumerable<AccountData> accounts = null;

            if (customerId.HasValue)
            {
                accounts = await accountsQueries.Get(customerId.Value);
                return Ok(accounts);
            }

            accounts = await accountsQueries.GetAll();
            return Ok(accounts);
        }
    }
}
