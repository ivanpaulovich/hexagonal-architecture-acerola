using MediatR;
using MyAccountAPI.Domain.Model.Accounts;
using System;
using System.Runtime.Serialization;

namespace MyAccountAPI.Producer.Application.Commands.Accounts
{
    [DataContract]
    public class DepositCommand : CommandBase, IRequest<Transaction>
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
