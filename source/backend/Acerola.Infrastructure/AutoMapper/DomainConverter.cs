namespace Acerola.Infrastructure.AutoMapper
{
    using Acerola.Application.DTO;
    using Acerola.Domain.Accounts;
    using Acerola.Domain.Customers;
    using global::AutoMapper;

    public class DomainConverter
    {
        private readonly MapperConfiguration config;
        
        public DomainConverter()
        {
            Mapper.Initialize(cfg => {
                cfg.AddProfile<AccountsProfile>();
                cfg.AddProfile<CustomersProfile>();
            });
        }

        public AccountData Map(Account account)
        {
            AccountData vm = Mapper.Map<AccountData>(account);
            return vm;
        }

        public CustomerData Map(Customer customer)
        {
            CustomerData vm = Mapper.Map<CustomerData>(customer);
            return vm;
        }
    }
}
