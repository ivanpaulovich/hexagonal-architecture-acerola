using Acerola.Domain.Customers;
using MediatR;
using System.Runtime.Serialization;

namespace Acerola.Application.Commands.Customers
{
    [DataContract]
    public class RegisterCustomerCommand : CommandBase, IRequest<Customer>
    {
        [DataMember]
        public string PIN { get; private set; }

        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public double InitialAmount { get; private set; }

        public RegisterCustomerCommand()
        {

        }
    }
}
