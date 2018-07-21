namespace Acerola.WebApi.UseCases.CloseAccount
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Acerola.Application.Commands.Close;

    [Route("api/[controller]")]
    public sealed class AccountsController : Controller
    {
        private readonly ICloseAccountUseCase closeService;

        public AccountsController(
            ICloseAccountUseCase closeService)
        {
            this.closeService = closeService;
        }

        /// <summary>
        /// Close an account
        /// </summary>
        [HttpDelete("{accountId}")]
        public async Task<IActionResult> Close(Guid accountId)
        {
            Guid closeResult = await closeService.Execute(accountId);

            if (closeResult == Guid.Empty)
            {
                return new NoContentResult();
            }

            return Ok();
        }
    }
}
