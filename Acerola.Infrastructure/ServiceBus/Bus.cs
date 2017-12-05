using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using MyAccountAPI.Domain.Model;
using MyAccountAPI.Domain.ServiceBus;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyAccountAPI.Producer.Infrastructure.ServiceBus
{
    public class Bus : IPublisher
    {
        public readonly string brokerList;
        public readonly string topic;

        private readonly Producer<string, string> _producer;

        public Bus(string brokerList, string topic)
        {
            this.brokerList = brokerList;
            this.topic = topic;

            _producer = new Producer<string, string>(
                new Dictionary<string, object>()
                {{
                    "bootstrap.servers",
                    brokerList
                }},
                new StringSerializer(Encoding.UTF8), new StringSerializer(Encoding.UTF8));
        }

        public async Task Publish(DomainEvent domainEvent)
        {
            string data = JsonConvert.SerializeObject(domainEvent, Formatting.Indented);

            Message<string, string> message = await _producer.ProduceAsync(
                topic, domainEvent.GetType().AssemblyQualifiedName, data);
        }

        public async Task Publish(IEnumerable<DomainEvent> domainEvents, Header header)
        {
            foreach (var domainEvent in domainEvents)
            {
                domainEvent.SetHeader(header);
                await Publish(domainEvent);
            }
        }

        public void Dispose()
        {
            _producer.Dispose();
        }
    }
}
