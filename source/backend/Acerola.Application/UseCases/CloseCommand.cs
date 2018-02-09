namespace Acerola.Application.UseCases
{
    using MediatR;
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class CloseCommand : IRequest
    {
        [DataMember]
        public Guid AccountId { get; set; }
    }
}
