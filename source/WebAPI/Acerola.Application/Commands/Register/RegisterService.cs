namespace Acerola.Application.Commands.Register
{
    using System.Threading.Tasks;
    using Acerola.Domain.Customers;
    using Acerola.Application.Repositories;
    using Acerola.Domain.Accounts;

    public sealed class RegisterService : IRegisterService
    {
        private readonly ICustomerWriteOnlyRepository customerWriteOnlyRepository;
        private readonly IAccountWriteOnlyRepository accountWriteOnlyRepository;

        public RegisterService(
            ICustomerWriteOnlyRepository customerWriteOnlyRepository,
            IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            this.customerWriteOnlyRepository = customerWriteOnlyRepository;
            this.accountWriteOnlyRepository = accountWriteOnlyRepository;
        }

        public async Task<RegisterResult> Process(RegisterCommand command)
        {
            Customer customer = new Customer(command.Personnummer, command.Name);

            Account account = new Account(customer.Id);
            account.Deposit(command.InitialAmount);
            Credit credit = (Credit)account.GetLastTransaction();

            customer.Register(account.Id);

            await customerWriteOnlyRepository.Add(customer);
            await accountWriteOnlyRepository.Add(account, credit);

            RegisterResult result = new RegisterResult(customer, account);
            return result;
        }
    }
}
