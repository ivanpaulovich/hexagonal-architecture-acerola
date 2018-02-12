namespace Acerola.Application.Commands.Register
{
    using System.Threading.Tasks;
    using Acerola.Domain.Customers;
    using Acerola.Domain.Customers.Accounts;
    using Acerola.Domain.ValueObjects;

    public class RegisterHandler : IRegisterHandler
    {
        private readonly ICustomerWriteOnlyRepository customerWriteOnlyRepository;

        public RegisterHandler(
            ICustomerWriteOnlyRepository customerWriteOnlyRepository)
        {
            this.customerWriteOnlyRepository = customerWriteOnlyRepository;
        }

        public async Task<Customer> Handle(RegisterCommand command)
        {
            Customer customer = new Customer(new PIN(command.PIN), new Name(command.Name));

            Account account = new Account();
            Credit credit = new Credit(new Amount(command.InitialAmount));
            account.Deposit(credit);

            customer.Register(account);

            await customerWriteOnlyRepository.Add(customer);

            return customer;
        }
    }
}
