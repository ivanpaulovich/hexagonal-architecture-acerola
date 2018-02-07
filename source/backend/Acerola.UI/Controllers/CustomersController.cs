namespace Acerola.UI.Controllers
{
    using Acerola.Application.Commands.Customers;
    using Acerola.Application.Queries;
    using Acerola.Application.ViewModels;
    using Acerola.Domain.Customers;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
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

            CustomerVM result = new CustomerVM
            {
                CustomerId = customer.Id,
                Name = customer.Name.Text,
                Personnummer = customer.PIN.Text
            };

            return CreatedAtRoute("GetCustomer", new { id = result.CustomerId }, result);
        }

        /// <summary>
        /// Get a Customer details 
        /// </summary>
        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<IActionResult> GetCustomer(Guid id)
        {
            CustomerVM customer = await customersQueries.GetCustomer(id);

            return Ok(customer);
        }

        /// <summary>
        /// List all customers
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<CustomerVM> customers = await customersQueries.GetAll();

            return Ok(customers);
        }
    }
}
