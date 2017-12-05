using MediatR;
using System.Runtime.Serialization;
using MyAccountAPI.Domain.Model.Customers;

namespace MyAccountAPI.Producer.Application.Commands.Customers
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
