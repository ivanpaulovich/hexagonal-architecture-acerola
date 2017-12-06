using Autofac;
using Acerola.Domain.ServiceBus;
using MyAccountAPI.Producer.Infrastructure.ServiceBus;

namespace MyAccountAPI.Producer.Infrastructure.Modules
{
    public class BusModule : Module
    {
        private readonly string brokerList;
        private readonly string topic;

        public BusModule(string brokerList, string topic)
        {
            this.brokerList = brokerList;
            this.topic = topic;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Bus>()
                .As<IPublisher>()
                .WithParameter("brokerList", brokerList)
                .WithParameter("topic", topic)
                .SingleInstance();
        }
    }
}
