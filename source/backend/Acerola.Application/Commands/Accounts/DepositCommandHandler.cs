namespace Acerola.Application.CommandHandlers.Accounts
{
    using MediatR;
    using System;
    using System.Threading.Tasks;
    using Acerola.Application.Commands.Accounts;
    using Acerola.Domain.Accounts;
    using Acerola.Domain.ValueObjects;

    public class DepositCommandHandler : IAsyncRequestHandler<DepositCommand, Transaction>
    {
        private readonly IAccountReadOnlyRepository accountReadOnlyRepository;
        private readonly IAccountWriteOnlyRepository accountWriteOnlyRepository;

        public DepositCommandHandler(
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

        public async Task<Transaction> Handle(DepositCommand command)
        {
            Account account = await accountReadOnlyRepository.Get(command.AccountId);
            if (account == null)
                throw new AccountNotFoundException($"The account {command.AccountId} does not exists or is already closed.");

            Credit credit = Credit.Create(Amount.Create(command.Amount));
            account.Deposit(credit);

            await accountWriteOnlyRepository.Update(account);

            return credit;
        }
    }
}
