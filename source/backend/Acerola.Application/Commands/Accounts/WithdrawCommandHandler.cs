namespace Acerola.Application.CommandHandlers.Accounts
{
    using MediatR;
    using System;
    using System.Threading.Tasks;
    using Acerola.Application.Commands.Accounts;
    using Acerola.Domain.Accounts;
    using Acerola.Domain.ValueObjects;

    public class WithdrawCommandHandler : IAsyncRequestHandler<WithdrawCommand, Transaction>
    {
        private readonly IAccountReadOnlyRepository accountReadOnlyRepository;
        private readonly IAccountWriteOnlyRepository accountWriteOnlyRepository;

        public WithdrawCommandHandler(
            IAccountReadOnlyRepository accountReadOnlyRepository,
            IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            if (accountReadOnlyRepository == null)
                throw new ArgumentNullException(nameof(accountReadOnlyRepository));

            if (accountWriteOnlyRepository == null)
                throw new ArgumentNullException(nameof(accountWriteOnlyRepository));

            this.accountReadOnlyRepository = accountReadOnlyRepository;
            this.accountWriteOnlyRepository = accountWriteOnlyRepository;
        }

        public async Task<Transaction> Handle(WithdrawCommand command)
        {
            Account account = await accountReadOnlyRepository.Get(command.AccountId);
            if (account == null)
                throw new AccountNotFoundException($"The account {command.AccountId} does not exists or is already closed.");

            Transaction transaction = Debit.Create(command.CustomerId, Amount.Create(command.Amount));
            account.Withdraw(transaction);

            await accountWriteOnlyRepository.Update(account);

            return transaction;
        }
    }
}
