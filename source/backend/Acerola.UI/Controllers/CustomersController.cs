namespace Acerola.UI.Controllers
{
    using Acerola.Application.Commands.Customers;
    using Acerola.Application.Queries;
    using Acerola.Domain.Customers;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System;
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

            dynamic loadedCustomer = (dynamic)await customersQueries.GetAsync(customer.Id);

            return CreatedAtRoute("GetCustomer", new { id = loadedCustomer._id }, loadedCustomer);
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
