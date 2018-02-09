namespace Acerola.Application.UseCases
{
    using Acerola.Domain.Customers;
    using MediatR;
    using System.Runtime.Serialization;

    [DataContract]
    public class RegisterCommand : IRequest<Customer>
    {
        [DataMember]
        public string PIN { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public double InitialAmount { get; set; }
    }
}
