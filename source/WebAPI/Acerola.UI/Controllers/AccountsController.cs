namespace Acerola.UI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Acerola.Application.Queries;
    using Acerola.Application.Commands.Close;
    using Acerola.Application.Commands.Withdraw;
    using Acerola.Application.Commands.Deposit;
    using Acerola.UI.Requests;
    using Acerola.UI.Model;

    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IDepositService depositService;
        private readonly IWithdrawService withdrawService;
        private readonly ICloseService closeService;
        private readonly IAccountsQueries accountsQueries;

        public AccountsController(
            IDepositService depositService, 
            IWithdrawService withdrawService,
            ICloseService closeService,
            IAccountsQueries accountsQueries)
        {
            this.depositService = depositService;
            this.withdrawService = withdrawService;
            this.closeService = closeService;
            this.accountsQueries = accountsQueries;
        }

        /// <summary>
        /// Deposit from an account
        /// </summary>
        [HttpPatch("Deposit")]
        public async Task<IActionResult> Deposit([FromBody]DepositRequest request)
        {
            var command = new DepositCommand(
                request.AccountId,
                request.Amount);

            DepositResult depositResult = await depositService.Process(command);

            if (depositResult == null)
            {
                return new NoContentResult();
            }

            DepositModel model = new DepositModel(
                depositResult.Transaction.Amount,
                depositResult.Transaction.Description,
                depositResult.Transaction.TransactionDate,
                depositResult.UpdatedBalance
            );

            return new ObjectResult(model);
        }

        /// <summary>
        /// Withdraw from an account
        /// </summary>
        [HttpPatch("Withdraw")]
        public async Task<IActionResult> Withdraw([FromBody]WithdrawRequest request)
        {
            var command = new WithdrawCommand(
                request.AccountId,
                request.Amount);

            WithdrawResult depositResult = await withdrawService.Process(command);

            if (depositResult == null)
            {
                return new NoContentResult();
            }

            WithdrawModel model = new WithdrawModel(
                depositResult.Transaction.Amount,
                depositResult.Transaction.Description,
                depositResult.Transaction.TransactionDate,
                depositResult.UpdatedBalance
            );

            return new ObjectResult(model);
        }

        /// <summary>
        /// Close an account
        /// </summary>
        [HttpDelete("{accountId}")]
        public async Task<IActionResult> Close(Guid accountId)
        {
            var command = new CloseCommand(accountId);

            CloseResult closeResult = await closeService.Process(command);

            if (closeResult == null)
            {
                return new NoContentResult();
            }

            return Ok();
        }

        /// <summary>
        /// Get an account balance
        /// </summary>
        [HttpGet("{accountId}", Name = "GetAccount")]
        public async Task<IActionResult> Get(Guid accountId)
        {
            var account = await accountsQueries.GetAccount(accountId);

            return Ok(account);
        }
    }
}
