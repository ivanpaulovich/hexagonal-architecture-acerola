namespace Acerola.WebApi.UseCases.Register
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Acerola.Application.Commands.Register;
    using Acerola.WebApi.Model;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly IRegisterService registerService;

        public CustomersController(IRegisterService registerService)
        {
            this.registerService = registerService;
        }

        /// <summary>
        /// Register a new Customer
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegisterRequest request)
        {
            var command = new RegisterCommand(request.Personnummer, request.Name, request.InitialAmount);
            RegisterResult result = await registerService.Process(command);

            List<TransactionModel> transactions = new List<TransactionModel>();

            foreach (var item in result.Account.Transactions)
            {
                var transaction = new TransactionModel(
                    item.Amount,
                    item.Description,
                    item.TransactionDate);

                transactions.Add(transaction);
            }

            AccountDetailsModel account = new AccountDetailsModel(
                result.Account.AccountId,
                result.Account.CurrentBalance,
                transactions);

            List<AccountDetailsModel> accounts = new List<AccountDetailsModel>();
            accounts.Add(account);

            Model model = new Model(
                result.Customer.CustomerId,
                result.Customer.Personnummer,
                result.Customer.Name,
                accounts
            );

            return CreatedAtRoute("GetCustomer", new { customerId = model.CustomerId }, model);
        }
    }
}
