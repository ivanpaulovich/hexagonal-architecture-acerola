namespace Acerola.Application.Commands.Withdraw
{
    using System.Threading.Tasks;
    using Acerola.Application.Results;
    using Acerola.Domain.ValueObjects;
    using Acerola.Application.Repositories;
    using Acerola.Domain.Accounts;

    public class WithdrawService : IWithdrawService
    {
        private readonly IAccountReadOnlyRepository accountReadOnlyRepository;
        private readonly IAccountWriteOnlyRepository accountWriteOnlyRepository;
        private readonly IResultConverter resultConverter;

        public WithdrawService(
            IAccountReadOnlyRepository accountReadOnlyRepository,
            IAccountWriteOnlyRepository accountWriteOnlyRepository,
            IResultConverter resultConverter)
        {
            this.accountReadOnlyRepository = accountReadOnlyRepository;
            this.accountWriteOnlyRepository = accountWriteOnlyRepository;
            this.resultConverter = resultConverter;
        }

        public async Task<WithdrawResult> Process(WithdrawCommand command)
        {
            Account account = await accountReadOnlyRepository.Get(command.AccountId);
            if (account == null)
                throw new AccountNotFoundException($"The account {command.AccountId} does not exists or is already closed.");

            Debit debit = new Debit(account.Id, command.Amount);
            account.Withdraw(debit);

            await accountWriteOnlyRepository.Update(account, debit);

            TransactionResult transactionResult = resultConverter.Map<TransactionResult>(debit);
            WithdrawResult result = new WithdrawResult(
                transactionResult,
                account.GetCurrentBalance().Value
            );

            return result;
        }
    }
}
