using System;
using Xunit;
using System.Linq;
using Acerola.Domain.Accounts;
using Acerola.Domain.Customers;
using Acerola.Domain.ValueObjects;

namespace Acerola.Domain.UnitTests
{
    public class DomainTests
    {
        [Fact]
        public void Register_Valid_User_Account()
        {
            //
            // Arrange
            Customer sut = Customer.Create(
                PIN.Create("08724050601"),
                Name.Create("Ivan Paulovich"));

            Account account = Account.Create(
                sut, 
                Amount.Create(1000.0));

            //
            // Act
            sut.Register(account);

            //
            // Assert
            var accounts = sut.Accounts;
            var registered = accounts.Where(e => e.Id == account.Id).First();

            Assert.Equal(registered.CurrentBalance.Value, 1000.0);
        }

        [Fact]
        public void Deposit_100()
        {
            //
            // Arrange
            Customer customer = Customer.Create(
                PIN.Create("08724050601"),
                Name.Create("Ivan Paulovich"));

            Account account = Account.Create(
                customer,
                Amount.Create(1000.0));

            customer.Register(account);

            Account sut = customer.Accounts.First();

            Transaction transaction = Credit.Create(
                customer.Id, Amount.Create(100.0));

            //
            // Act
            sut.Deposit(transaction);

            //
            // Assert
            var transactions = sut.Transactions;
            var deposited = transactions.Where(e => e.Id == transaction.Id).First();

            Assert.Equal(deposited.Amount.Value, 100.0);
            Assert.Equal(sut.CurrentBalance.Value, 1100);
        }

        [Fact]
        public void Withdraw_100()
        {
            //
            // Arrange
            Customer customer = Customer.Create(
                PIN.Create("08724050601"),
                Name.Create("Ivan Paulovich"));

            Account account = Account.Create(
                customer,
                Amount.Create(1000.0));

            customer.Register(account);

            Account sut = customer.Accounts.First();

            Transaction transaction = Debit.Create(
                customer.Id, Amount.Create(100.0));

            //
            // Act
            sut.Withdraw(transaction);

            //
            // Assert
            var transactions = sut.Transactions;
            var deposited = transactions.Where(e => e.Id == transaction.Id).First();

            Assert.Equal(deposited.Amount.Value, 100.0);
            Assert.Equal(sut.CurrentBalance.Value, 900);
        }

        [Fact]
        public void Close_Account()
        {
            //
            // Arrange
            Customer customer = Customer.Create(
                PIN.Create("08724050601"),
                Name.Create("Ivan Paulovich"));

            Account account = Account.Create(
                customer,
                Amount.Create(1000.0));

            customer.Register(account);

            Account sut = customer.Accounts.First();
            Transaction transaction = Debit.Create(
                customer.Id, Amount.Create(1000.0));

            sut.Withdraw(transaction);

            //
            // Act
            sut.Close();

            //
            // Assert
            var transactions = sut.Transactions;
            var closed = transactions.Where(e => e.Id == transaction.Id).First();

            Assert.Equal(closed.Amount.Value, 1000.0);
        }

        [Fact]
        public void Close_Account_With_Funds()
        {
            //
            // Arrange
            Customer customer = Customer.Create(
                PIN.Create("08724050601"),
                Name.Create("Ivan Paulovich"));

            Account account = Account.Create(
                customer,
                Amount.Create(1000.0));

            customer.Register(account);

            Account sut = customer.Accounts.First();

            //
            // Act and Assert
            Assert.Throws<AccountCannotBeClosedException>(
                () => sut.Close());
        }


        [Fact]
        public void Withdraw_More_Than_Current_Balance()
        {
            //
            // Arrange
            Customer customer = Customer.Create(
                PIN.Create("08724050601"),
                Name.Create("Ivan Paulovich"));

            Account account = Account.Create(
                customer,
                Amount.Create(1000.0));

            customer.Register(account);

            Account sut = customer.Accounts.First();

            Transaction transaction = Debit.Create(
                customer.Id, Amount.Create(5000.0));

            //
            // Act and Assert
            Assert.Throws<InsuficientFundsException>(
                () => sut.Withdraw(transaction));
        }
    }
}
