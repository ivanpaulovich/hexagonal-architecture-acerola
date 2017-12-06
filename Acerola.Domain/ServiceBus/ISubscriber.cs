using MediatR;
using System;

namespace Acerola.Domain.ServiceBus
{
    public interface ISubscriber : IDisposable
    {
        void Listen(IMediator mediator);
    }
}
