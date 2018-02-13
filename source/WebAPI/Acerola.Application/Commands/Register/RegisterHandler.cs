namespace Acerola.Application.Commands.Register
{
    using System.Threading.Tasks;
    using Acerola.Application.Results;
    using Acerola.Domain.Customers;
    using Acerola.Domain.Customers.Accounts;
    using Acerola.Domain.ValueObjects;

    public class RegisterHandler : IRegisterHandler
    {
        private readonly ICustomerWriteOnlyRepository customerWriteOnlyRepository;
        private readonly IResultConverter resultConverter;

        public RegisterHandler(
            ICustomerWriteOnlyRepository customerWriteOnlyRepository,
            IResultConverter resultConverter)
        {
            this.customerWriteOnlyRepository = customerWriteOnlyRepository;
            this.resultConverter = resultConverter;
        }

        public async Task<RegisterResult> Handle(RegisterCommand command)
        {
            Customer customer = new Customer(new PIN(command.PIN), new Name(command.Name));

            Account account = new Account();
            Credit credit = new Credit(new Amount(command.InitialAmount));
            account.Deposit(credit);

            customer.Register(account);

            await customerWriteOnlyRepository.Add(customer);

            CustomerResult customerResponse = resultConverter.Map<CustomerResult>(customer);
            AccountResult accountResponse = resultConverter.Map<AccountResult>(account);
            RegisterResult response = new RegisterResult(customerResponse, accountResponse);

            return response;
        }
    }
}
