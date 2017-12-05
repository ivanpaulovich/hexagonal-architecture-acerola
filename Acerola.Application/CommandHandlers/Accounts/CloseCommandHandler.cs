using MediatR;
using System;
using System.Threading.Tasks;
using Acerola.Domain.ServiceBus;
using Acerola.Commands.Accounts;
using Acerola.Domain.Model.Accounts;
using Acerola.Domain.Exceptions;

namespace Acerola.Application.CommandHandlers.Accounts
{
    public class CloseCommandHandler : IAsyncRequestHandler<CloseCommand>
    {
        private readonly IPublisher bus;
        private readonly IAccountReadOnlyRepository accountReadOnlyRepository;

        public CloseCommandHandler(
            IPublisher bus,
            IAccountReadOnlyRepository accountReadOnlyRepository)
        {
            if (bus == null)
                throw new ArgumentNullException(nameof(bus));

            if (accountReadOnlyRepository == null)
                throw new ArgumentNullException(nameof(accountReadOnlyRepository));

            this.bus = bus;
            this.accountReadOnlyRepository = accountReadOnlyRepository;
        }

        public async Task Handle(CloseCommand command)
        {
            Account account = await accountReadOnlyRepository.Get(command.AccountId);
            if (account == null)
                throw new AccountNotFoundException($"The account {command.AccountId} does not exists or is already closed.");

            account.Close();

            var domainEvents = account.GetEvents();
            await bus.Publish(domainEvents, command.Header);
        }
    }
}
