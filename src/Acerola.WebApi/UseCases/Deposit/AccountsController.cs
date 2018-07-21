namespace Acerola.WebApi.UseCases.Deposit
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Acerola.Application.Commands.Deposit;

    [Route("api/[controller]")]
    public sealed class AccountsController : Controller
    {
        private readonly IDepositUseCase depositService;

        public AccountsController(
            IDepositUseCase depositService)
        {
            this.depositService = depositService;
        }

        /// <summary>
        /// Deposit from an account
        /// </summary>
        [HttpPatch("Deposit")]
        public async Task<IActionResult> Deposit([FromBody]DepositRequest request)
        {
            DepositResult depositResult = await depositService.Execute(
                request.AccountId,
                request.Amount);

            if (depositResult == null)
            {
                return new NoContentResult();
            }

            Model model = new Model(
                depositResult.Transaction.Amount,
                depositResult.Transaction.Description,
                depositResult.Transaction.TransactionDate,
                depositResult.UpdatedBalance
            );

            return new ObjectResult(model);
        }
    }
}
