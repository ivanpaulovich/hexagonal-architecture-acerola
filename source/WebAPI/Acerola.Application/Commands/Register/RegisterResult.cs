namespace Acerola.Application.Commands.Register
{
    using Acerola.Application.Results;
    using Acerola.Domain.Accounts;
    using Acerola.Domain.Customers;
    using System.Collections.Generic;

    public sealed class RegisterResult
    {
        public CustomerResult Customer { get; }
        public AccountResult Account { get; }

        public RegisterResult(Customer customer, Account account)
        {
            List<TransactionResult> transactionResults = new List<TransactionResult>();

            foreach (ITransaction transaction in account.Transactions)
            {
                transactionResults.Add(
                    new TransactionResult(
                        transaction.Description,
                        transaction.Amount,
                        transaction.TransactionDate));
            }

            Account = new AccountResult(account.Id, account.GetCurrentBalance(), transactionResults);

            List<AccountResult> accountResults = new List<AccountResult>();
            accountResults.Add(Account);

            Customer = new CustomerResult(customer.Id, customer.SSN, customer.Name, accountResults);
        }
    }
}
