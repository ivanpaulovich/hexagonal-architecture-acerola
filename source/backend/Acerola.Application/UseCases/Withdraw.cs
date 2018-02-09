namespace Acerola.Application.UseCases
{
    using MediatR;
    using System;
    using System.Threading.Tasks;
    using Acerola.Domain.Accounts;
    using Acerola.Domain.ValueObjects;

    public class Withdraw : IAsyncRequestHandler<WithdrawCommand, Debit>
    {
        private readonly IAccountReadOnlyRepository accountReadOnlyRepository;
        private readonly IAccountWriteOnlyRepository accountWriteOnlyRepository;

        public Withdraw(
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

        public async Task<Debit> Handle(WithdrawCommand command)
        {
            Account account = await accountReadOnlyRepository.Get(command.AccountId);
            if (account == null)
                throw new AccountNotFoundException($"The account {command.AccountId} does not exists or is already closed.");

            Debit debit = Debit.Create(Amount.Create(command.Amount));
            account.Withdraw(debit);

            await accountWriteOnlyRepository.Update(account);

            return debit;
        }
    }
}
