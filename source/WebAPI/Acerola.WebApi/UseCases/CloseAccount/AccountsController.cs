namespace Acerola.WebApi.UseCases.CloseAccount
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Acerola.Application.Commands.Close;

    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly ICloseService closeService;

        public AccountsController(
            ICloseService closeService)
        {
            this.closeService = closeService;
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
    }
}
