namespace Acerola.UI.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Acerola.Domain.Customers;
    using Acerola.Application.Commands.Customers;
    using Acerola.Application.Queries;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly IMediator mediator;
        private readonly ICustomersQueries customersQueries;

        public CustomersController(IMediator mediator, ICustomersQueries customersQueries)
        {
            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator));

            if (customersQueries == null)
                throw new ArgumentNullException(nameof(customersQueries));

            this.mediator = mediator;
            this.customersQueries = customersQueries;
        }

        /// <summary>
        /// Register a new Customer
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegisterCustomerCommand command)
        {
            Customer customer = await mediator.Send(command);

            var result = new
            {
                CustomerId = customer.Id,
                SSN = customer.PIN.Text,
                Name = customer.Name.Text,
                AccountId = customer.Accounts.First().Id,
                CurrentBalance = customer.Accounts.First().CurrentBalance
            };

            return CreatedAtRoute("GetCustomer", new { id = customer.Id }, result);
        }

        /// <summary>
        /// Get a Customer details 
        /// </summary>
        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<IActionResult> GetCustomer(Guid id)
        {
            var customer = await customersQueries.GetAsync(id);

            return Ok(customer);
        }

        /// <summary>
        /// List all customers
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = await customersQueries.GetAsync();

            return Ok(customers);
        }
    }
}
