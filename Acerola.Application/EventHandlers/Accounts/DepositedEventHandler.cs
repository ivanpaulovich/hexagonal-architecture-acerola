using Acerola.Domain.Accounts;
using Acerola.Domain.Accounts.Events;
using MediatR;
using System;

namespace Acerola.Application.EventHandlers.Blogs
{
    public class DepositedEventHandler : IRequestHandler<DepositedDomainEvent>
    {
        private readonly IAccountReadOnlyRepository accountReadOnlyRepository;
        private readonly IAccountWriteOnlyRepository accountWriteOnlyRepository;

        public DepositedEventHandler(
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

        public void Handle(DepositedDomainEvent domainEvent)
        {
            Account account = accountReadOnlyRepository.Get(domainEvent.AggregateRootId).Result;

            if (account == null)
                throw new AccountNotFoundException($"The account {domainEvent.AggregateRootId} does not exists or is already closed.");
            
            //if (account.Version != domainEvent.Version)
            //    throw new TransactionConflictException(account, domainEvent);

            account.Apply(domainEvent);
            accountWriteOnlyRepository.Update(account).Wait();
        }
    }
}
