using MyAccountAPI.Domain.Exceptions;
using MyAccountAPI.Domain.Model.ValueObjects;
using Xunit;

namespace MyAccountAPI.Domain.UnitTests
{
    public class ValueObjectsTests
    {
        [Fact]
        public void Empty_Name()
        {
            //
            // Arrange
            string empty = string.Empty;

            //
            // Act and Assert
            Assert.Throws<NameShouldNotBeEmptyException>(
                () => Name.Create(empty));
        }

        [Fact]
        public void Valid_Name()
        {
            //
            // Arrange
            string valid = "Ivan Paulovich";

            //
            // Act
            Name name = Name.Create(valid);

            //
            // Assert
            Assert.Equal(valid, name.Text);
        }

        [Fact]
        public void Empty_PIN()
        {
            //
            // Arrange
            string empty = string.Empty;

            //
            // Act and Assert
            Assert.Throws<PINShouldNotBeEmptyException>(
                () => PIN.Create(empty));
        }

        [Fact]
        public void Valid_PIN()
        {
            //
            // Arrange
            string valid = "08724050601";

            //
            // Act
            PIN pin = PIN.Create(valid);

            // Assert
            Assert.Equal(valid, pin.Text);
        }

        [Fact]
        public void Negative_Amount()
        {
            //
            // Arrange
            double negative = -500;

            //
            // Act and Assert
            Assert.Throws<AmountShouldBePositiveException>(
                () => Amount.Create(negative));
        }

        [Fact]
        public void Valid_Amount()
        {
            //
            // Arrange
            double positive = 500;

            //
            // Act
            Amount amount = Amount.Create(positive);

            //
            // Assert
            Assert.Equal(positive, amount.Value);
        }
    }
}
