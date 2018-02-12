namespace Acerola.Application.Commands.Withdraw
{
    using System.Threading.Tasks;
    using Acerola.Domain.Customers;
    using Acerola.Domain.Customers.Accounts;
    using Acerola.Domain.ValueObjects;

    public class WithdrawHandler : IWithdrawHandler
    {
        private readonly ICustomerReadOnlyRepository customerReadOnlyRepository;
        private readonly ICustomerWriteOnlyRepository customerWriteOnlyRepository;

        public WithdrawHandler(
            ICustomerReadOnlyRepository customerReadOnlyRepository,
            ICustomerWriteOnlyRepository customerWriteOnlyRepository)
        {
            this.customerReadOnlyRepository = customerReadOnlyRepository;
            this.customerWriteOnlyRepository = customerWriteOnlyRepository;
        }

        public async Task<Debit> Handle(WithdrawCommand command)
        {
            Customer customer = await customerReadOnlyRepository.GetByAccount(command.AccountId);
            if (customer == null)
                throw new AccountNotFoundException($"The account {command.AccountId} does not exists or is already closed.");

            Debit debit = new Debit(new Amount(command.Amount));
            Account account = customer.FindAccount(command.AccountId);
            account.Withdraw(debit);

            await customerWriteOnlyRepository.Update(customer);

            return debit;
        }
    }
}
