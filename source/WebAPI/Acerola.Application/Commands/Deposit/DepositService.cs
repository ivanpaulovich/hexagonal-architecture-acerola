namespace Acerola.Application.Commands.Deposit
{
    using System.Threading.Tasks;
    using Acerola.Application.Results;
    using Acerola.Domain.Customers;
    using Acerola.Domain.Customers.Accounts;
    using Acerola.Domain.ValueObjects;

    public class DepositService : IDepositService
    {
        private readonly ICustomerReadOnlyRepository customerReadOnlyRepository;
        private readonly ICustomerWriteOnlyRepository customerWriteOnlyRepository;
        private readonly IResultConverter resultConverter;

        public DepositService(
            ICustomerReadOnlyRepository customerReadOnlyRepository,
            ICustomerWriteOnlyRepository customerWriteOnlyRepository,
            IResultConverter resultConverter)
        {
            this.customerReadOnlyRepository = customerReadOnlyRepository;
            this.customerWriteOnlyRepository = customerWriteOnlyRepository;
            this.resultConverter = resultConverter;
        }

        public async Task<DepositResult> Process(DepositCommand command)
        {
            Customer customer = await customerReadOnlyRepository.GetByAccount(command.AccountId);
            if (customer == null)
                throw new AccountNotFoundException($"The account {command.AccountId} does not exists or is already closed.");

            Credit credit = new Credit(new Amount(command.Amount));
            Account account = customer.FindAccount(command.AccountId);
            account.Deposit(credit);

            await customerWriteOnlyRepository.Update(customer);

            TransactionResult transactionResult = resultConverter.Map<TransactionResult>(credit);
            DepositResult response = new DepositResult(transactionResult, account.CurrentBalance.Value);

            return response;
        }
    }
}
