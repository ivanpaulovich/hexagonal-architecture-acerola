namespace Acerola.Infrastructure.Queries
{
    using Acerola.Application.Queries;
    using MongoDB.Driver;
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Acerola.Infrastructure.DataAccess;
    using Acerola.Domain.Customers;
    using Acerola.Application;
    using Acerola.Application.Results;

    public class CustomersQueries : ICustomersQueries
    {
        private readonly AccountBalanceContext mongoDB;
        private readonly IResultConverter mapper;

        public CustomersQueries(AccountBalanceContext mongoDB, IResultConverter mapper)
        {
            this.mongoDB = mongoDB;
            this.mapper = mapper;
        }

        public async Task<CustomerResult> GetCustomer(Guid id)
        {
            Customer data = await this.mongoDB.Customers
                .Find(Builders<Customer>.Filter.Eq("_id", id))
                .SingleOrDefaultAsync();

            if (data == null)
                throw new CustomerNotFoundException($"The account {id} does not exists or is not processed yet.");

            CustomerResult customerVM = this.mapper.Map<CustomerResult>(data);

            return customerVM;
        }
    }
}
