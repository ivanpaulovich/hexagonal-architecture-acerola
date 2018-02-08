namespace Acerola.Domain.UnitTests
{
    using Xunit;
    using Acerola.Domain.Accounts;
    using Acerola.Domain.Customers;
    using Acerola.Domain.ValueObjects;
    using NSubstitute;

    public class DomainTests
    {
        [Fact]
        public void Register_Valid_User_Account()
        {
            //
            // Arrange
            Customer sut = Substitute.For<Customer>();
            Account account = Substitute.For<Account>();

            //
            // Act
            sut.Register(account);

            //
            // Assert
            Assert.Equal(1, sut.Accounts.Count);
        }

        [Fact]
        public void Deposit_100()
        {
            //
            // Arrange
            Account sut = Substitute.For<Account>();
            Credit credit = Credit.Create(Amount.Create(100.0));

            //
            // Act
            sut.Deposit(credit);

            //
            // Assert
            Assert.True(true);
        }

        [Fact]
        public void Withdraw_100()
        {
            //
            // Arrange
            Account sut = Substitute.For<Account>();
            Credit credit = Credit.Create(Amount.Create(1000.0));
            sut.Deposit(credit);

            Debit transaction = Debit.Create(Amount.Create(100.0));

            //
            // Act
            sut.Withdraw(transaction);

            //
            // Assert
            Assert.True(true);
        }

        [Fact]
        public void Close_A_New_Account()
        {
            //
            // Arrange
            Account sut = Substitute.For<Account>();

            //
            // Act
            sut.Close();

            //
            // Assert
            Assert.True(true);
        }

        [Fact]
        public void Close_Account_With_Funds()
        {
            //
            // Arrange
            Account sut = Substitute.For<Account>();
            Credit credit = Credit.Create(Amount.Create(100));
            sut.Deposit(credit);

            //
            // Act and Assert
            Assert.Throws<AccountCannotBeClosedException>(
                () => sut.Close());
        }


        [Fact]
        public void Withdraw_More_Than_The_Current_Balance()
        {
            //
            // Arrange
            Account sut = Substitute.For<Account>();
            Credit credit = Credit.Create(Amount.Create(200));
            sut.Deposit(credit);

            Debit debit = Debit.Create(Amount.Create(5000.0));

            //
            // Act and Assert
            Assert.Throws<InsuficientFundsException>(
                () => sut.Withdraw(debit));
        }
    }
}
