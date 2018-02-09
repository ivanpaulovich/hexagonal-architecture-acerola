namespace Acerola.Application.UseCases
{
    using MediatR;
    using System;
    using System.Runtime.Serialization;
    using Acerola.Domain.Accounts;

    [DataContract]
    public class WithdrawCommand : IRequest<Debit>
    {
        [DataMember]
        public Guid AccountId { get; set; }

        [DataMember]
        public Double Amount { get; set; }
    }
}
