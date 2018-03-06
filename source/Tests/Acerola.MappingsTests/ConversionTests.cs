namespace Acerola.MappingsTests
{
    using Acerola.Application;
    using Acerola.Application.Results;
    using Acerola.Domain.Accounts;
    using Acerola.Domain.ValueObjects;
    using Acerola.Infrastructure.Mappings;
    using Xunit;

    public class ConversionTests
    {
        public IResultConverter converter;

        public ConversionTests()
        {
            converter = new ResultConverter();
        }

        [Fact]
        public void Convert_Debit_Valid_TransactionResponse()
        {
            Debit debit = new Debit(new Amount(100));

            var result = converter.Map<TransactionResult>(debit);
            Assert.Equal(debit.Amount.Value, result.Amount);
            Assert.Equal(debit.TransactionDate, result.TransactionDate);
            Assert.Equal(debit.Description, result.Description);
        }
    }
}
