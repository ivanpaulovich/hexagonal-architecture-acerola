namespace Acerola.Application.UseCases
{
    using MediatR;
    using System;
    using System.Threading.Tasks;
    using Acerola.Domain.Accounts;
    using Acerola.Domain.ValueObjects;

    public class Deposit : IAsyncRequestHandler<DepositCommand, Credit>
    {
        private readonly IAccountReadOnlyRepository accountReadOnlyRepository;
        private readonly IAccountWriteOnlyRepository accountWriteOnlyRepository;

        public Deposit(
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

        public async Task<Credit> Handle(DepositCommand command)
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
