namespace Acerola.Application.Commands.Withdraw
{
    using Acerola.Application.Results;
    using Acerola.Domain.Accounts;
    using Acerola.Domain.ValueObjects;

    public class WithdrawResult
    {
        public TransactionResult Transaction { get; }
        public double UpdatedBalance { get; }

        public WithdrawResult(Debit transaction, Amount updatedBalance)
        {
            Transaction = new TransactionResult(
                transaction.Description,
                transaction.Amount,
                transaction.TransactionDate);

            UpdatedBalance = updatedBalance;
        }
    }
}
