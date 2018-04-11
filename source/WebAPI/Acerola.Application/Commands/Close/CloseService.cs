namespace Acerola.Application.Commands.Close
{
    using System.Threading.Tasks;
    using Acerola.Application.Repositories;
    using Acerola.Domain.Accounts;

    public class CloseService : ICloseService
    {
        private readonly IAccountReadOnlyRepository accountReadOnlyRepository;
        private readonly IAccountWriteOnlyRepository accountWriteOnlyRepository;
        private readonly IResultConverter resultConverter;

        public CloseService(
            IAccountReadOnlyRepository accountReadOnlyRepository,
            IAccountWriteOnlyRepository accountWriteOnlyRepository,
            IResultConverter resultConverter)
        {
            this.accountReadOnlyRepository = accountReadOnlyRepository;
            this.accountWriteOnlyRepository = accountWriteOnlyRepository;
            this.resultConverter = resultConverter;
        }

        public async Task<CloseResult> Process(CloseCommand command)
        {
            Account account = await accountReadOnlyRepository.Get(command.AccountId);
			if (account == null)
                throw new AccountNotFoundException($"The account {command.AccountId} does not exists or is already closed.");
			
            account.Close();

            await accountWriteOnlyRepository.Delete(account);

            CloseResult result = resultConverter.Map<CloseResult>(account);

            return result;
        }
    }
}
