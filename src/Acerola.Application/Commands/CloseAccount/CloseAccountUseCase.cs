namespace Acerola.Application.Commands.Close
{
    using System;
    using System.Threading.Tasks;
    using Acerola.Application.Repositories;
    using Acerola.Domain.Accounts;

    public sealed class CloseAccountUseCase : ICloseAccountUseCase
    {
        private readonly IAccountReadOnlyRepository accountReadOnlyRepository;
        private readonly IAccountWriteOnlyRepository accountWriteOnlyRepository;

        public CloseAccountUseCase(
            IAccountReadOnlyRepository accountReadOnlyRepository,
            IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            this.accountReadOnlyRepository = accountReadOnlyRepository;
            this.accountWriteOnlyRepository = accountWriteOnlyRepository;
        }

        public async Task<Guid> Execute(Guid accountId)
        {
            Account account = await accountReadOnlyRepository.Get(accountId);
			if (account == null)
                throw new AccountNotFoundException($"The account {accountId} does not exists or is already closed.");
			
            account.Close();

            await accountWriteOnlyRepository.Delete(account);

            return account.Id;
        }
    }
}
