namespace Acerola.Infrastructure.Queries
{
    using Acerola.Application.Queries;
    using MongoDB.Driver;
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Acerola.Infrastructure.DataAccess;
    using Acerola.Application.ViewModels;
    using Acerola.Domain.Customers;

    public class CustomersQueries : ICustomersQueries
    {
        private readonly AccountBalanceContext mongoDB;

        public CustomersQueries(AccountBalanceContext mongoDB)
        {
            this.mongoDB = mongoDB;
        }

        public async Task<IEnumerable<CustomerVM>> GetAll()
        {
            IEnumerable<Customer> data = await this.mongoDB.Customers
                .Find(e => true)
                .ToListAsync();

            List<CustomerVM> result = new List<CustomerVM>();

            foreach (Customer item in data)
            {
                CustomerVM customerVM = new CustomerVM()
                {
                    CustomerId = item.Id,
                    Personnummer = item.PIN.Text,
                    Name = item.Name.Text
                };

                result.Add(customerVM);
            }

            return result;
        }

        public async Task<CustomerVM> GetCustomer(Guid id)
        {
            Customer data = await this.mongoDB.Customers
                .Find(Builders<Customer>.Filter.Eq("_id", id))
                .SingleOrDefaultAsync();

            if (data == null)
                throw new CustomerNotFoundException($"The account {id} does not exists or is not processed yet.");

            CustomerVM customerVM = new CustomerVM()
            {
                CustomerId = data.Id,
                Personnummer = data.PIN.Text,
                Name = data.Name.Text
            };

            return customerVM;
        }
    }
}
