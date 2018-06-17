namespace Acerola.Application.Commands.Close
{
    using System.Threading.Tasks;
    using Acerola.Application.Repositories;
    using Acerola.Domain.Accounts;

    public sealed class CloseService : ICloseService
    {
        private readonly IAccountReadOnlyRepository accountReadOnlyRepository;
        private readonly IAccountWriteOnlyRepository accountWriteOnlyRepository;

        public CloseService(
            IAccountReadOnlyRepository accountReadOnlyRepository,
            IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            this.accountReadOnlyRepository = accountReadOnlyRepository;
            this.accountWriteOnlyRepository = accountWriteOnlyRepository;
        }

        public async Task<CloseResult> Process(CloseCommand command)
        {
            Account account = await accountReadOnlyRepository.Get(command.AccountId);
			if (account == null)
                throw new AccountNotFoundException($"The account {command.AccountId} does not exists or is already closed.");
			
            account.Close();

            await accountWriteOnlyRepository.Delete(account);

            CloseResult result = new CloseResult(account);
            return result;
        }
    }
}
