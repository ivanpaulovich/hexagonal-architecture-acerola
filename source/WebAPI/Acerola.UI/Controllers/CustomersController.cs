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
            RegisterResult registerResult = await registerService.Handle(command);

            RegisterModel model = new RegisterModel(
                registerResult.Customer.CustomerId,
                registerResult.Customer.Personnummer,
                registerResult.Customer.Name
            );

            return CreatedAtRoute("GetCustomer", new { id = model.CustomerId }, model);
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
