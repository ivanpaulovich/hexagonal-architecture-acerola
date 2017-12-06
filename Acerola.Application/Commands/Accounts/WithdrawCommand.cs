using MediatR;
using System;
using System.Runtime.Serialization;
using Acerola.Domain.Accounts;

namespace Acerola.Application.Commands.Accounts
{
    [DataContract]
    public class WithdrawCommand : CommandBase, IRequest<Transaction>
    {
        [DataMember]
        public Guid CustomerId { get; private set; }

        [DataMember]
        public Guid AccountId { get; private set; }

        [DataMember]
        public Double Amount { get; private set; }

        public WithdrawCommand()
        {

        }
    }
}
