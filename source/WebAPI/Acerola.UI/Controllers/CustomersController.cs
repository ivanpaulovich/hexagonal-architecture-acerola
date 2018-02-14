namespace Acerola.UI.Controllers
{
    using Acerola.Application.Queries;
    using Acerola.Application.Results;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Acerola.UI.Requests;
    using Acerola.Application.Commands.Register;
    using Acerola.UI.Model;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly ICustomersQueries customersQueries;
        private readonly IRegisterService registerService;

        public CustomersController(ICustomersQueries customersQueries,
            IRegisterService registerService)
        {
            this.customersQueries = customersQueries;
            this.registerService = registerService;
        }

        /// <summary>
        /// Register a new Customer
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegisterRequest request)
        {
            var command = new RegisterCommand(request.PIN, request.Name, request.InitialAmount);
            RegisterResult result = await registerService.Handle(command);

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

            RegisterModel model = new RegisterModel(
                result.Customer.CustomerId,
                result.Customer.Personnummer,
                result.Customer.Name,
                accounts
            );

            return CreatedAtRoute("GetCustomer", new { customerId = model.CustomerId }, model);
        }

        /// <summary>
        /// Get a Customer details 
        /// </summary>
        [HttpGet("{customerId}", Name = "GetCustomer")]
        public async Task<IActionResult> GetCustomer(Guid customerId)
        {
            CustomerResult customer = await customersQueries.GetCustomer(customerId);

            return Ok(customer);
        }
    }
}
