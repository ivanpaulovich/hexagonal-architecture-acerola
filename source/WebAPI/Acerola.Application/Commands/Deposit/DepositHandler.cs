namespace Acerola.Application.Commands.Deposit
{
    using System.Threading.Tasks;
    using Acerola.Domain.Customers;
    using Acerola.Domain.Customers.Accounts;
    using Acerola.Domain.ValueObjects;

    public class DepositHandler : IDepositHandler
    {
        private readonly ICustomerReadOnlyRepository customerReadOnlyRepository;
        private readonly ICustomerWriteOnlyRepository customerWriteOnlyRepository;

        public DepositHandler(
            ICustomerReadOnlyRepository customerReadOnlyRepository,
            ICustomerWriteOnlyRepository customerWriteOnlyRepository)
        {
            this.customerReadOnlyRepository = customerReadOnlyRepository;
            this.customerWriteOnlyRepository = customerWriteOnlyRepository;
        }

        public async Task<Credit> Handle(DepositCommand command)
        {
            Customer customer = await customerReadOnlyRepository.GetByAccount(command.AccountId);
            if (customer == null)
                throw new AccountNotFoundException($"The account {command.AccountId} does not exists or is already closed.");

            Credit credit = new Credit(new Amount(command.Amount));
            Account account = customer.FindAccount(command.AccountId);
            account.Deposit(credit);

            await customerWriteOnlyRepository.Update(customer);

            return credit;
        }
    }
}
