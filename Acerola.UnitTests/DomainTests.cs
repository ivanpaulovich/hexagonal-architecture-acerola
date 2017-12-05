using MyAccountAPI.Domain.Model.Accounts;
using MyAccountAPI.Domain.Model.Customers;
using MyAccountAPI.Domain.Model.ValueObjects;
using System;
using Xunit;
using System.Linq;
using MyAccountAPI.Domain.Model.Customers.Events;
using MyAccountAPI.Domain.Model.Accounts.Events;
using MyAccountAPI.Domain.Exceptions;

namespace MyAccountAPI.Domain.UnitTests
{
    public class DomainTests
    {
        [Fact]
        public void Register_Valid_User_Account()
        {
            //
            // Arrange
            Account account = Account.Create(
                Guid.NewGuid(), 
                Amount.Create(1000.0));

            Customer sut = Customer.Create(
                PIN.Create("08724050601"),
                Name.Create("Ivan Paulovich"));

            //
            // Act
            sut.Register(account);

            //
            // Assert
            var domainEvents = sut.GetEvents();
            var registered = domainEvents.Where(e => e is RegisteredDomainEvent).First() as RegisteredDomainEvent;

            Assert.Equal(registered.InitialAmount.Value, 1000.0);
        }

        [Fact]
        public void Deposit_100()
        {
            //
            // Arrange
            Account account = Account.Create(
                Guid.NewGuid(),
                Amount.Create(1000.0));

            Customer customer = Customer.Create(
                PIN.Create("08724050601"),
                Name.Create("Ivan Paulovich"));

            customer.Register(account);

            Account sut = customer.GetAccounts().First();

            Transaction transaction = Credit.Create(
                customer.Id, Amount.Create(100.0));

            //
            // Act
            sut.Deposit(transaction);

            //
            // Assert
            var domainEvents = sut.GetEvents();
            var deposited = domainEvents.Where(e => e is DepositedDomainEvent).First() as DepositedDomainEvent;

            Assert.Equal(deposited.Amount.Value, 100.0);
            Assert.Equal(sut.GetCurrentBalance().Value, 1100);
        }

        [Fact]
        public void Withdraw_100()
        {
            //
            // Arrange
            Account account = Account.Create(
                Guid.NewGuid(),
                Amount.Create(1000.0));

            Customer customer = Customer.Create(
                PIN.Create("08724050601"),
                Name.Create("Ivan Paulovich"));

            customer.Register(account);

            Account sut = customer.GetAccounts().First();

            Transaction transaction = Debit.Create(
                customer.Id, Amount.Create(100.0));

            //
            // Act
            sut.Withdraw(transaction);

            //
            // Assert
            var domainEvents = sut.GetEvents();
            var deposited = domainEvents.Where(e => e is WithdrewDomainEvent).First() as WithdrewDomainEvent;

            Assert.Equal(deposited.Amount.Value, 100.0);
            Assert.Equal(sut.GetCurrentBalance().Value, 900);
        }

        [Fact]
        public void Close_Account()
        {
            //
            // Arrange
            Account account = Account.Create(
                Guid.NewGuid(),
                Amount.Create(1000.0));

            Customer customer = Customer.Create(
                PIN.Create("08724050601"),
                Name.Create("Ivan Paulovich"));

            customer.Register(account);

            Account sut = customer.GetAccounts().First();
            Transaction transaction = Debit.Create(
                customer.Id, Amount.Create(1000.0));
            sut.Withdraw(transaction);

            //
            // Act
            sut.Close();

            //
            // Assert
            var domainEvents = sut.GetEvents();
            var closed = domainEvents.Where(e => e is ClosedDomainEvent).Count();

            Assert.NotEqual(closed, 0);
        }

        [Fact]
        public void Close_Account_With_Funds()
        {
            //
            // Arrange
            Account account = Account.Create(
                Guid.NewGuid(),
                Amount.Create(1000.0));

            Customer customer = Customer.Create(
                PIN.Create("08724050601"),
                Name.Create("Ivan Paulovich"));

            customer.Register(account);

            Account sut = customer.GetAccounts().First();

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
            Account account = Account.Create(
                Guid.NewGuid(),
                Amount.Create(1000.0));

            Customer customer = Customer.Create(
                PIN.Create("08724050601"),
                Name.Create("Ivan Paulovich"));

            customer.Register(account);

            Account sut = customer.GetAccounts().First();

            Transaction transaction = Debit.Create(
                customer.Id, Amount.Create(5000.0));

            //
            // Act and Assert
            Assert.Throws<InsuficientFundsException>(
                () => sut.Withdraw(transaction));
        }
    }
}
