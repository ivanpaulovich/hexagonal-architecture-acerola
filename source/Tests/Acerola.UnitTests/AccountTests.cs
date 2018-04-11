namespace Acerola.UnitTests
{
    using Xunit;
    using Acerola.Domain.ValueObjects;
    using NSubstitute;
    using Acerola.Domain.Accounts;
    using System;

    public class AccountTests
    {
        [Fact]
        public void New_Account_Should_Have_100_After_Deposit()
        {
            //
            // Arrange
            Account sut = new Account(Guid.NewGuid());
            Credit credit = new Credit(Guid.NewGuid(), new Amount(100.0));

            //
            // Act
            sut.Deposit(credit);

            //
            // Assert
            Assert.Equal(100, sut.GetCurrentBalance().Value);
        }

        [Fact]
        public void New_Account_With_1000_Balance_Should_Have_900_After_Withdraw()
        {
            //
            // Arrange
            Account sut = new Account(Guid.NewGuid());
            Credit credit = new Credit(Guid.NewGuid(), new Amount(1000.0));
            sut.Deposit(credit);

            Debit transaction = new Debit(Guid.NewGuid(), new Amount(100.0));

            //
            // Act
            sut.Withdraw(transaction);

            //
            // Assert
            Assert.Equal(900, sut.GetCurrentBalance().Value);
        }

        [Fact]
        public void New_Account_Should_Allow_Close()
        {
            //
            // Arrange
            Account sut = new Account(Guid.NewGuid());

            //
            // Act
            sut.Close();

            //
            // Assert
            Assert.True(true);
        }

        [Fact]
        public void Account_With_Funds_Should_Not_Allow_Close()
        {
            //
            // Arrange
            Account sut = new Account(Guid.NewGuid());
            Credit credit = new Credit(Guid.NewGuid(), new Amount(100));
            sut.Deposit(credit);

            //
            // Act and Assert
            Assert.Throws<AccountCannotBeClosedException>(
                () => sut.Close());
        }


        [Fact]
        public void Account_With_200_Balance_Should_Not_Allow_50000_Withdraw()
        {
            //
            // Arrange
            Account sut = new Account(Guid.NewGuid());
            Credit credit = new Credit(Guid.NewGuid(), new Amount(200));
            sut.Deposit(credit);

            Debit debit = new Debit(Guid.NewGuid(), new Amount(5000.0));

            //
            // Act and Assert
            Assert.Throws<InsuficientFundsException>(
                () => sut.Withdraw(debit));
        }
    }
}
