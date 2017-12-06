using Acerola.Domain.Accounts.Events;
using Acerola.Domain.Customers.Events;
using Acerola.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Acerola.Domain.Accounts
{
    public class Account : AggregateRoot
    {
        private Guid customerId;
        private Amount currentBalance;

        public Guid GetCustomerId()
        {
            return customerId;
        }

        public Amount GetCurrentBalance()
        {
            return currentBalance;
        }

        private List<Transaction> transactions;

        public IReadOnlyCollection<Transaction> GetTransactions()
        {
            return transactions;
        }

        private Account()
        {
            Register<DepositedDomainEvent>(When);
            Register<WithdrewDomainEvent>(When);
            Register<RegisteredDomainEvent>(When);
            Register<ClosedDomainEvent>(When);
        }

        public static Account Create(Guid customerId, Amount initialAmount)
        {
            if (initialAmount == null)
                throw new ArgumentNullException(nameof(initialAmount));

            Account account = new Account();
            account.customerId = customerId;
            account.currentBalance = initialAmount;
            return account;
        }

        public static Account Create(Guid customerId)
        {
            Account account = new Account();
            account.customerId = customerId;
            return account;
        }

        public static Account Load(Guid accountId, Amount initialAmount)
        {
            if (initialAmount == null)
                throw new ArgumentNullException(nameof(initialAmount));

            Account account = new Account();
            account.Id = accountId;
            account.currentBalance = initialAmount;
            return account;
        }

        public void Deposit(Transaction transaction)
        {
            Raise(DepositedDomainEvent.Create(this, transaction.Id, transaction.GetCustomerId(), transaction.GetAmount()));
        }

        public void Withdraw(Transaction transaction)
        {
            if (GetCurrentBalance() < transaction.GetAmount())
                throw new InsuficientFundsException($"The account {Id} does not have enough funds to withdraw {transaction.GetAmount()}.");

            Raise(WithdrewDomainEvent.Create(this, transaction.Id, transaction.GetCustomerId(), transaction.GetAmount()));
        }

        protected void When(DepositedDomainEvent domainEvent)
        {
            if (transactions == null)
                transactions = new List<Transaction>();

            Transaction transaction = Credit.Load(domainEvent.TransactionId, domainEvent.CustomerId, domainEvent.Amount);
            transactions.Add(transaction);

            currentBalance = currentBalance + domainEvent.Amount;
        }

        protected void When(WithdrewDomainEvent domainEvent)
        {
            if (transactions == null)
                transactions = new List<Transaction>();

            Transaction transaction = Debit.Load(domainEvent.TransactionId, domainEvent.CustomerId, domainEvent.Amount);
            transactions.Add(transaction);

            currentBalance = currentBalance - domainEvent.Amount;
        }

        protected void When(RegisteredDomainEvent domainEvent)
        {
            if (domainEvent == null)
                throw new ArgumentNullException(nameof(domainEvent));

            Id = domainEvent.AccountId;
            currentBalance = domainEvent.InitialAmount;
        }

        public void Close()
        {
            if (GetCurrentBalance() > Amount.Create(0))
                throw new AccountCannotBeClosedException($"The account {Id} can not be closed because it has funds.");

            Raise(ClosedDomainEvent.Create(this));
        }

        protected void When(ClosedDomainEvent domainEvent)
        {

        }
    }
}
