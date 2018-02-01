namespace Acerola.Application.Commands.Accounts
{
    using MediatR;
    using System;
    using System.Runtime.Serialization;
    using Acerola.Domain.Accounts;

    [DataContract]
    public class DepositCommand : IRequest<Transaction>
    {
        [DataMember]
        public Guid CustomerId { get; private set; }

        [DataMember]
        public Guid AccountId { get; private set; }

        [DataMember]
        public Double Amount { get; private set; }

        public DepositCommand()
        {

        }
    }
}
