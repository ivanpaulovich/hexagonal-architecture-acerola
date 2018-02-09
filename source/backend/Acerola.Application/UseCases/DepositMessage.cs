namespace Acerola.Application.UseCases
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class DepositMessage
    {
        [DataMember]
        public Guid AccountId { get; set; }

        [DataMember]
        public Double Amount { get; set; }
    }
}
