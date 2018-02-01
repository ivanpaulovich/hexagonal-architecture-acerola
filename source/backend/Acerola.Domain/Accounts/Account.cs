namespace Acerola.Domain.Accounts
{
    using Acerola.Domain.Customers;
    using Acerola.Domain.ValueObjects;
    using System;
    using System.Collections.Generic;

    public class Account : AggregateRoot
    {
        private Guid customerId;
        private Amount currentBalance;
        private List<Transaction> transactions;

        public Guid GetCustomerId()
        {
            return customerId;
        }

        public Amount GetCurrentBalance()
        {
            return currentBalance;
        }

        public IReadOnlyCollection<Transaction> GetTransactions()
        {
            return transactions;
        }

        public static Account Create(Customer customer, Amount initialAmount)
        {
            if (initialAmount == null)
                throw new ArgumentNullException(nameof(initialAmount));

            Account account = new Account();
            account.customerId = customer.Id;
            account.currentBalance = initialAmount;
            return account;
        }

        public void Deposit(Transaction transaction)
        {
            if (transactions == null)
                transactions = new List<Transaction>();

            transactions.Add(transaction);

            currentBalance = currentBalance + transaction.GetAmount();
        }

        public void Withdraw(Transaction transaction)
        {
            if (GetCurrentBalance() < transaction.GetAmount())
                throw new InsuficientFundsException($"The account {Id} does not have enough funds to withdraw {transaction.GetAmount()}.");

            if (transactions == null)
                transactions = new List<Transaction>();

            transactions.Add(transaction);

            currentBalance = currentBalance - transaction.GetAmount();
        }

        public void Close()
        {
            if (GetCurrentBalance() > Amount.Create(0))
                throw new AccountCannotBeClosedException($"The account {Id} can not be closed because it has funds.");
        }
    }
}
