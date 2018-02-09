namespace Acerola.Application.UseCases
{
    using MediatR;
    using System;
    using System.Threading.Tasks;
    using Acerola.Domain.Accounts;

    public class Close : IAsyncRequestHandler<CloseCommand>
    {
        private readonly IAccountReadOnlyRepository accountReadOnlyRepository;
        private readonly IAccountWriteOnlyRepository accountWriteOnlyRepository;

        public Close(
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

        public async Task Handle(CloseCommand command)
        {
            Account account = accountReadOnlyRepository.Get(command.AccountId).Result;

            await accountWriteOnlyRepository.Delete(account);
        }
    }
}
