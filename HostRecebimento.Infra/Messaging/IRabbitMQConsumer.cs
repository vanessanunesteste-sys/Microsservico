using HostRecebimento.Domain.Entities;

namespace HostRecebimento.Infra.Messaging
{
    public interface IRabbitMQConsumer
    {
        void Consume(Action<PessoaMensagem> onMessage);
    }
}
