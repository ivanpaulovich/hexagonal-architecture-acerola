namespace Acerola.UI.Controllers
{
    using Acerola.Application.Queries;
    using Acerola.Application.Results;
    using Acerola.Domain.Customers;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Acerola.UI.Requests;
    using Acerola.Application.Commands.Register;
    using Acerola.UI.Model;

    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly ICustomersQueries customersQueries;
        private readonly IRegisterHandler registerHandler;

        public CustomersController(ICustomersQueries customersQueries,
            IRegisterHandler registerHandler)
        {
            this.customersQueries = customersQueries;
            this.registerHandler = registerHandler;
        }

        /// <summary>
        /// Register a new Customer
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegisterRequest request)
        {
            var command = new RegisterCommand(request.PIN, request.Name, request.InitialAmount);
            Customer customer = await registerHandler.Handle(command);

            RegisterModel result = new RegisterModel(
                customer.Id,
                customer.PIN.Text,
                customer.Name.Text
            );

            return CreatedAtRoute("GetCustomer", new { id = customer.Id }, result);
        }

        /// <summary>
        /// Get a Customer details 
        /// </summary>
        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<IActionResult> GetCustomer(Guid id)
        {
            CustomerResult customer = await customersQueries.GetCustomer(id);

            return Ok(customer);
        }
    }
}
