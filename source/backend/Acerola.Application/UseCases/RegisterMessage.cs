namespace Acerola.Application.UseCases
{
    using System.Runtime.Serialization;

    [DataContract]
    public class RegisterMessage
    {
        [DataMember]
        public string PIN { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public double InitialAmount { get; set; }
    }
}
