namespace Acerola.WebApi.UseCases.Withdraw
{
    using System;
    public sealed class WithdrawRequest
    {
        public Guid AccountId { get; set; }
        public Double Amount { get; set; }
    }
}
