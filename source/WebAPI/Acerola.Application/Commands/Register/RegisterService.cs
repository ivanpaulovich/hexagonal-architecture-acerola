namespace Acerola.Application.Commands.Register
{
    using System.Threading.Tasks;
    using Acerola.Application.Results;
    using Acerola.Domain.Customers;
    using Acerola.Domain.ValueObjects;
    using Acerola.Application.Repositories;
    using Acerola.Domain.Accounts;

    public class RegisterService : IRegisterService
    {
        private readonly ICustomerWriteOnlyRepository customerWriteOnlyRepository;
        private readonly IAccountWriteOnlyRepository accountWriteOnlyRepository;
        private readonly IResultConverter resultConverter;

        public RegisterService(
            ICustomerWriteOnlyRepository customerWriteOnlyRepository,
            IAccountWriteOnlyRepository accountWriteOnlyRepository,
            IResultConverter resultConverter)
        {
            this.customerWriteOnlyRepository = customerWriteOnlyRepository;
            this.accountWriteOnlyRepository = accountWriteOnlyRepository;
            this.resultConverter = resultConverter;
        }

        public async Task<RegisterResult> Process(RegisterCommand command)
        {
            Customer customer = new Customer(command.PIN, command.Name);

            Account account = new Account(customer.Id);
            Credit credit = new Credit(account.Id, command.InitialAmount);
            account.Deposit(credit);

            customer.Register(account.Id);

            await customerWriteOnlyRepository.Add(customer);
            await accountWriteOnlyRepository.Add(account, credit);

            CustomerResult customerResult = resultConverter.Map<CustomerResult>(customer);
            AccountResult accountResult = resultConverter.Map<AccountResult>(account);
            RegisterResult result = new RegisterResult(customerResult, accountResult);

            return result;
        }
    }
}
