namespace Acerola.Application.UseCases
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class CloseMessage
    {
        [DataMember]
        public Guid AccountId { get; set; }
    }
}
