using MediatR;
using System;
using System.Threading.Tasks;
using MyAccountAPI.Domain.ServiceBus;
using MyAccountAPI.Producer.Application.Commands.Accounts;
using MyAccountAPI.Domain.Model.Accounts;
using MyAccountAPI.Domain.Model.ValueObjects;
using MyAccountAPI.Domain.Exceptions;

namespace MyAccountAPI.Producer.Application.CommandHandlers.Accounts
{
    public class WithdrawCommandHandler : IAsyncRequestHandler<WithdrawCommand, Transaction>
    {
        private readonly IPublisher bus;
        private readonly IAccountReadOnlyRepository accountReadOnlyRepository;

        public WithdrawCommandHandler(
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

        public async Task<Transaction> Handle(WithdrawCommand command)
        {
            Account account = await accountReadOnlyRepository.Get(command.AccountId);
            if (account == null)
                throw new AccountNotFoundException($"The account {command.AccountId} does not exists or is already closed.");

            Transaction transaction = Credit.Create(command.CustomerId, Amount.Create(command.Amount));
            account.Withdraw(transaction);

            var domainEvents = account.GetEvents();
            await bus.Publish(domainEvents, command.Header);

            return transaction;
        }
    }
}
