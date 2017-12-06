using MediatR;
using System;
using System.Runtime.Serialization;

namespace Acerola.Application.Commands.Accounts
{
    [DataContract]
    public class CloseCommand : CommandBase, IRequest
    {
        [DataMember]
        public Guid AccountId { get; private set; }

        public CloseCommand()
        {

        }
    }
}
